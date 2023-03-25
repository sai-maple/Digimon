using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Domain.Entity;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UniRx;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.UI
{
    public sealed class LoseScreenPresenter : IInitializable, IDisposable
    {
        private readonly ScreenEntity _screenEntity;
        private readonly MessageEntity _messageEntity;
        private readonly MonsterTypeEntity _monsterTypeEntity;
        private readonly ScreenView _screenView;
        private readonly EvolutionView _evolutionView;
        private readonly Screens _screens;

        private readonly CompositeDisposable _disposable = new();
        private readonly CancellationTokenSource _cancellation = new();

        public LoseScreenPresenter(ScreenEntity screenEntity, MessageEntity messageEntity,
            MonsterTypeEntity monsterTypeEntity, ScreenView screenView,
            EvolutionView evolutionView, Screens screens)
        {
            _screenEntity = screenEntity;
            _messageEntity = messageEntity;
            _monsterTypeEntity = monsterTypeEntity;
            _screenView = screenView;
            _evolutionView = evolutionView;
            _screens = screens;
        }

        public void Initialize()
        {
            _screenView.Initialize();
            _screenEntity.OnChangedAsObservable()
                .Subscribe(screen =>
                {
                    if (screen == _screens)
                    {
                        PresentAsync().Forget();
                    }
                    else
                    {
                        _screenView.DismissAsync().Forget();
                    }
                }).AddTo(_disposable);

            _monsterTypeEntity.OnEvolutionAsObservable()
                .Where(_ => _screenEntity.Value == _screens)
                .Subscribe(monster => DegenerationAsync(monster).Forget());
        }

        private async UniTaskVoid PresentAsync()
        {
            await _evolutionView.SpawnAsync(_monsterTypeEntity.Value);
            if (_cancellation.IsCancellationRequested) return;
            await _screenView.PresentAsync();
            if (_cancellation.IsCancellationRequested) return;

            // 敗北イベント 最後コマンドでlose
            _messageEntity.Result(BattleState.Lose);
        }

        private async UniTaskVoid DegenerationAsync(MonsterName monsterName)
        {
            await _evolutionView.EvolutionAsync(monsterName);
            // 最後コマンドでmenuにもどす
            _messageEntity.ToEvent($"Events/Battle/Result/Lose2").Forget();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _cancellation?.Cancel();
            _cancellation?.Dispose();
        }
    }
}
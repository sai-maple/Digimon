using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Applications.Static;
using Digimon.Digimon.Scripts.Domain.Entity;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UniRx;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.UI
{
    public sealed class EvolutionScreenPresenter : IInitializable, IDisposable
    {
        private readonly ScreenEntity _screenEntity;
        private readonly MonsterTypeEntity _monsterTypeEntity;
        private readonly MessageEntity _messageEntity;
        private readonly EvolutionView _evolutionView;
        private readonly PictorialBookEntity _pictorialBookEntity;
        private readonly ScreenView _screenView;
        private readonly Screens _screens;

        private readonly CompositeDisposable _disposable = new();
        private readonly CancellationTokenSource _cancellation = new();

        public EvolutionScreenPresenter(ScreenEntity screenEntity, MonsterTypeEntity monsterTypeEntity,
            MessageEntity messageEntity, EvolutionView evolutionView, PictorialBookEntity pictorialBookEntity,
            ScreenView screenView, Screens screens)
        {
            _screenEntity = screenEntity;
            _monsterTypeEntity = monsterTypeEntity;
            _messageEntity = messageEntity;
            _evolutionView = evolutionView;
            _pictorialBookEntity = pictorialBookEntity;
            _screenView = screenView;
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
                .Subscribe(name => EvolutionAsync(name).Forget());
        }

        private async UniTaskVoid PresentAsync()
        {
            await _evolutionView.SpawnAsync(_monsterTypeEntity.Value);
            if (_cancellation.IsCancellationRequested) return;
            await _screenView.PresentAsync();
            if (_cancellation.IsCancellationRequested) return;

            // 進化前メッセージの読み込み + コマンドで進化
            _messageEntity.ToEvent(_monsterTypeEntity.EvolutionFile(true)).Forget();
        }

        private async UniTaskVoid EvolutionAsync(MonsterName monsterName)
        {
            _pictorialBookEntity.Evolution(monsterName);
            await _evolutionView.EvolutionAsync(monsterName);
            _messageEntity.ToEvent(_monsterTypeEntity.EvolutionFile(false)).Forget();
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _cancellation.Cancel();
            _cancellation.Dispose();
        }
    }
}
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Domain.Entity;
using Digimon.Digimon.Scripts.Presentation.View.Monster;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using naichilab;
using UniRx;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.UI
{
    public sealed class BattleWinScreenPresenter : IInitializable, IDisposable
    {
        private readonly DateTimeEntity _dateTimeEntity;
        private readonly ScreenEntity _screenEntity;
        private readonly MonsterTypeEntity _monsterTypeEntity;
        private readonly MessageEntity _messageEntity;
        private readonly BattleEntity _battleEntity;
        private readonly MonsterSpawner _monsterSpawner;
        private readonly WinScreenView _winScreenView;
        private readonly ScreenView _screenView;
        private readonly ConfirmView _confirmView;
        private readonly Screens _screens;

        private readonly CompositeDisposable _disposable = new();
        private readonly CancellationTokenSource _cancellation = new();

        public BattleWinScreenPresenter(DateTimeEntity dateTimeEntity, ScreenEntity screenEntity,
            MonsterTypeEntity monsterTypeEntity, MessageEntity messageEntity, BattleEntity battleEntity,
            MonsterSpawner monsterSpawner, WinScreenView winScreenView, ScreenView screenView, ConfirmView confirmView,
            Screens screens)
        {
            _dateTimeEntity = dateTimeEntity;
            _screenEntity = screenEntity;
            _monsterTypeEntity = monsterTypeEntity;
            _messageEntity = messageEntity;
            _battleEntity = battleEntity;
            _monsterSpawner = monsterSpawner;
            _winScreenView = winScreenView;
            _screenView = screenView;
            _confirmView = confirmView;
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

            _winScreenView.OnRankingTapAsObservable()
                .Subscribe(_ => SceneManager.LoadScene("Ranking", LoadSceneMode.Additive))
                .AddTo(_disposable);

            _winScreenView.OnReturnTapAsObservable()
                .Subscribe(_ => _confirmView.Present(() => SceneManager.LoadScene($"Tutorial")))
                .AddTo(_disposable);

            _battleEntity.OnStateChangedAsObservable()
                .Where(state => state == BattleState.Win2)
                .Subscribe(_ => PresentUI());
        }

        private async UniTaskVoid PresentAsync()
        {
            _winScreenView.Initialize();
            await _monsterSpawner.SpawnAsync(_monsterTypeEntity.Value);
            if (_cancellation.IsCancellationRequested) return;
            await _screenView.PresentAsync();
            if (_cancellation.IsCancellationRequested) return;

            // おわかれイベント
            _messageEntity.Result(BattleState.Win);
        }

        private async void PresentUI()
        {
            await _winScreenView.Present(_dateTimeEntity.Date);
            RankingLoader.Instance.SendScoreAndShowRanking(_dateTimeEntity.Date);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _cancellation?.Cancel();
            _cancellation?.Dispose();
        }
    }
}
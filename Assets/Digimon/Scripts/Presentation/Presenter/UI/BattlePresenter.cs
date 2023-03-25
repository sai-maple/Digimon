using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Domain.Entity;
using Digimon.Digimon.Scripts.Domain.UseCase;
using Digimon.Digimon.Scripts.Presentation.View.Battle;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UniRx;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.UI
{
    public sealed class BattlePresenter : IInitializable, IDisposable
    {
        private readonly MonsterTypeEntity _monsterTypeEntity;
        private readonly ScreenEntity _screenEntity;
        private readonly BattleEntity _battleEntity;
        private readonly BattleUseCase _battleUseCase;
        private readonly BattleView _battleView;
        private readonly ScreenView _screenView;
        private readonly Screens _screens;

        private readonly CompositeDisposable _disposable = new();
        private readonly CancellationTokenSource _cancellation = new();

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

            // バトル進行
            _battleEntity.OnStateChangedAsObservable()
                .Subscribe(OnStateChanged)
                .AddTo(_disposable);

            // ダメージ演出
            _battleEntity.OnSelfHpChangedAsObservable()
                .Subscribe(value => _battleView.TakeDamage(value.Damage, value.Hp))
                .AddTo(_disposable);

            _battleEntity.OnEnemyHpChangedAsObservable()
                .Subscribe(value => _battleView.Attack(value.Damage, value.Hp))
                .AddTo(_disposable);
        }

        private async UniTaskVoid PresentAsync()
        {
            var (selfHp, enemyHp) = _battleUseCase.Initialize();
            await _battleView.InitializeAsync(_monsterTypeEntity.Value, selfHp, enemyHp);
            if (_cancellation.IsCancellationRequested) return;
            await _screenView.PresentAsync();
        }

        private async void OnStateChanged(BattleState state)
        {
            if (state == BattleState.BattleStart) await _battleView.BattleStartAsync();
            _battleUseCase.OnNext(state);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _cancellation?.Cancel();
            _cancellation?.Dispose();
        }
    }
}
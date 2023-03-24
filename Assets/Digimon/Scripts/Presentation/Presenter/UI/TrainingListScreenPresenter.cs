using System;
using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Domain.Entity;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UniRx;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.UI
{
    public sealed class TrainingListScreenPresenter : IInitializable, IDisposable
    {
        private readonly ScreenEntity _screenEntity;
        private readonly StatusEntity _statusEntity;
        private readonly StaminaEntity _staminaEntity;
        private readonly TrainingListView _trainingListView;
        private readonly ScreenView _screenView;
        private readonly Screens _screens;

        private readonly CompositeDisposable _disposable = new();

        public TrainingListScreenPresenter(ScreenEntity screenEntity, StatusEntity statusEntity,
            StaminaEntity staminaEntity, TrainingListView trainingListView, ScreenView screenView, Screens screens)
        {
            _screenEntity = screenEntity;
            _statusEntity = statusEntity;
            _staminaEntity = staminaEntity;
            _trainingListView = trainingListView;
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
                        Present();
                    }
                    else
                    {
                        _screenView.DismissAsync().Forget();
                    }
                }).AddTo(_disposable);

            _trainingListView.OnBackAsObservable()
                .Subscribe(_ => _screenEntity.OnNext(Screens.Menu))
                .AddTo(_disposable);

            // 戻る
            _trainingListView.OnHpTrainingAsObservable()
                .Subscribe(OnTraining)
                .AddTo(_disposable);
            _trainingListView.OnAtkTrainingAsObservable()
                .Subscribe(OnTraining)
                .AddTo(_disposable);
            _trainingListView.OnDefTrainingAsObservable()
                .Subscribe(OnTraining)
                .AddTo(_disposable);
            _trainingListView.OnSpeedTrainingAsObservable()
                .Subscribe(OnTraining)
                .AddTo(_disposable);
        }

        private void Present()
        {
            _trainingListView.Initialize(
                _staminaEntity.ClashRate(TrainingType.Hp, _statusEntity.Hp),
                _staminaEntity.ClashRate(TrainingType.Atk, _statusEntity.Atk),
                _staminaEntity.ClashRate(TrainingType.Def, _statusEntity.Def),
                _staminaEntity.ClashRate(TrainingType.Speed, _statusEntity.Speed));
            _screenView.PresentAsync().Forget();
        }

        private void OnTraining(TrainingType trainingType)
        {
            var screen = trainingType switch
            {
                TrainingType.Hp => Screens.TrainingHp,
                TrainingType.Atk => Screens.TrainingAtk,
                TrainingType.Def => Screens.TrainingDef,
                TrainingType.Speed => Screens.TrainingSpeed,
                TrainingType.Crash => throw new ArgumentOutOfRangeException(nameof(trainingType), trainingType, null),
                _ => throw new ArgumentOutOfRangeException(nameof(trainingType), trainingType, null)
            };
            _screenEntity.OnNext(screen);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
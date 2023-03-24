using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Domain.Entity;
using Digimon.Digimon.Scripts.Domain.UseCase;
using Digimon.Digimon.Scripts.Extension;
using Digimon.Digimon.Scripts.Presentation.View.Monster;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UniRx;
using UnityEngine.Playables;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.UI
{
    public sealed class TrainingScreenPresenter : IInitializable, IDisposable
    {
        private readonly MonsterTypeEntity _monsterTypeEntity;
        private readonly MessageEntity _messageEntity;
        private readonly ScreenEntity _screenEntity;
        private readonly TrainingUseCase _trainingUseCase;
        private readonly MonsterSpawner _monsterSpawner;
        private readonly ScreenView _screenView;
        private readonly PlayableDirector _playableDirector;
        private readonly TrainingType _trainingType;
        private readonly Screens _screens;

        private readonly CompositeDisposable _disposable = new();
        private readonly CancellationTokenSource _cancellation = new();

        public TrainingScreenPresenter(MonsterTypeEntity monsterTypeEntity, MessageEntity messageEntity,
            ScreenEntity screenEntity, TrainingUseCase trainingUseCase, MonsterSpawner monsterSpawner,
            ScreenView screenView, PlayableDirector playableDirector, TrainingType trainingType, Screens screens)
        {
            _monsterTypeEntity = monsterTypeEntity;
            _messageEntity = messageEntity;
            _screenEntity = screenEntity;
            _trainingUseCase = trainingUseCase;
            _monsterSpawner = monsterSpawner;
            _screenView = screenView;
            _playableDirector = playableDirector;
            _trainingType = trainingType;
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
        }

        private async UniTaskVoid PresentAsync()
        {
            // 生成してから画面表示
            await _monsterSpawner.SpawnAsync(_monsterTypeEntity.Value);
            if (_cancellation.IsCancellationRequested) return;
            await _screenView.PresentAsync();
            if (_cancellation.IsCancellationRequested) return;
            await _playableDirector.PlayAsync(_cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;
            // 結果メッセージ読み込み + メッセージのコマンドによって 能力向上 + メニューに戻る or イベント発生
            _messageEntity.Training(_trainingType, _trainingUseCase.IsCrash(_trainingType));
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
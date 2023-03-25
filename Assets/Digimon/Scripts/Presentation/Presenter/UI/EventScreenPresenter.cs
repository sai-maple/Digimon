using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Domain.Entity;
using Digimon.Digimon.Scripts.Presentation.View.Monster;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UniRx;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.UI
{
    public sealed class EventScreenPresenter : IInitializable, IDisposable
    {
        private readonly MonsterTypeEntity _monsterTypeEntity;
        private readonly MessageEntity _messageEntity;
        private readonly ScreenEntity _screenEntity;
        private readonly MonsterSpawner _monsterSpawner;
        private readonly ScreenView _screenView;
        private readonly EventType _eventType;
        private readonly Screens _screens;

        private readonly CompositeDisposable _disposable = new();
        private readonly CancellationTokenSource _cancellation = new();

        public EventScreenPresenter(MonsterTypeEntity monsterTypeEntity, MessageEntity messageEntity,
            ScreenEntity screenEntity, MonsterSpawner monsterSpawner, ScreenView screenView,
            EventType eventType, Screens screens)
        {
            _monsterTypeEntity = monsterTypeEntity;
            _messageEntity = messageEntity;
            _screenEntity = screenEntity;
            _monsterSpawner = monsterSpawner;
            _screenView = screenView;
            _eventType = eventType;
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
            await _monsterSpawner.SpawnAsync(_monsterTypeEntity.Value);
            if (_cancellation.IsCancellationRequested) return;
            await _screenView.PresentAsync();
            if (_cancellation.IsCancellationRequested) return;

            // 結果メッセージ読み込み + メッセージのコマンドによって 能力向上 + メニューに戻る
            _messageEntity.RandomEvent(_eventType);
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _cancellation.Cancel();
            _cancellation.Dispose();
        }
    }
}
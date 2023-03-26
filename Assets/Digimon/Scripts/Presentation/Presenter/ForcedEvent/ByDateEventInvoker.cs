using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Domain.Entity;
using UniRx;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.ForcedEvent
{
    public sealed class ByDateEventInvoker : IInitializable, IDisposable
    {
        private readonly ScreenEntity _screenEntity;
        private readonly MessageEntity _messageEntity;
        private readonly DateTimeEntity _dateTimeEntity;
        private readonly MonsterAnimationEntity _animationEntity;

        private readonly CompositeDisposable _disposable = new();

        public ByDateEventInvoker(ScreenEntity screenEntity, MessageEntity messageEntity, DateTimeEntity dateTimeEntity,
            MonsterAnimationEntity animationEntity)
        {
            _screenEntity = screenEntity;
            _messageEntity = messageEntity;
            _dateTimeEntity = dateTimeEntity;
            _animationEntity = animationEntity;
        }

        public void Initialize()
        {
            //  初日の朝のイベント
            _messageEntity.ToEvent("Events/Events/ForcedEvent/FirstDay").Forget();

            // 10日目おきの試練イベント
            _dateTimeEntity.OnDateChangedAsObservable()
                .Where(date => date % 10 == 0)
                .Subscribe(_ => _screenEntity.OnNext(Screens.Battle));

            // 3.5.7日目の進化イベント
            _dateTimeEntity.OnDateChangedAsObservable()
                .Where(date => date % 10 == 3 || date % 10 == 5 || date % 10 == 7)
                .Subscribe(_ => _screenEntity.OnNext(Screens.Evolution));

            // 初日とイベント日以外は早朝イベント
            _dateTimeEntity.OnGameTimeChangedAsObservable()
                .Where(time => time == GameTime.Evening)
                .Subscribe(_ => EveningAsync().Forget());
        }

        private async UniTaskVoid EveningAsync()
        {
            var date = _dateTimeEntity.Date % 10;
            if ( date != 9 && date != 2 && date != 4 && date != 6)
            {
                _animationEntity.OnNext(MonsterReaction.Sleep);
                await UniTask.Delay(TimeSpan.FromSeconds(3));
                _messageEntity.Evening();
            }
            else
            {
                _animationEntity.OnNext(MonsterReaction.Sleep);
                await UniTask.Delay(TimeSpan.FromSeconds(3));
                // イベント日はそのまま起床
                _dateTimeEntity.Next();
            }
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
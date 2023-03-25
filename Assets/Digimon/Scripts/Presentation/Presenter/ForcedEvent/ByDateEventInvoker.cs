using System;
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

        private readonly CompositeDisposable _disposable = new();

        public ByDateEventInvoker(ScreenEntity screenEntity, MessageEntity messageEntity, DateTimeEntity dateTimeEntity)
        {
            _screenEntity = screenEntity;
            _messageEntity = messageEntity;
            _dateTimeEntity = dateTimeEntity;
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
                .Where(date => date is 3 or 5 or 7)
                .Subscribe(_ => _screenEntity.OnNext(Screens.Evolution));
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
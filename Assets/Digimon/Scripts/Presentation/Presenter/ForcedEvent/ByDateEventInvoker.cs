using Digimon.Digimon.Scripts.Domain.Entity;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.ForcedEvent
{
    public sealed class ByDateEventInvoker : IInitializable
    {
        private readonly ScreenEntity _screenEntity;
        private readonly MessageEntity _messageEntity;

        public ByDateEventInvoker(ScreenEntity screenEntity, MessageEntity messageEntity)
        {
            _screenEntity = screenEntity;
            _messageEntity = messageEntity;
        }

        public void Initialize()
        {
            //  初日の朝のイベント
            _messageEntity.ToEvent("Events/Events/ForcedEvent/FirstDay").Forget();
            
            // todo 10日目沖の試練イベント
            
            // todo 2.4.6日目の進化イベント
        }
    }
}
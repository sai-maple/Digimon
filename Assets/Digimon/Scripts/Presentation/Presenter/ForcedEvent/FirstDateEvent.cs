using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Domain.Entity;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.ForcedEvent
{
    public sealed class FirstDateEvent : IInitializable
    {
        private readonly ScreenEntity _screenEntity;
        private readonly MessageEntity _messageEntity;

        public FirstDateEvent(ScreenEntity screenEntity, MessageEntity messageEntity)
        {
            _screenEntity = screenEntity;
            _messageEntity = messageEntity;
        }

        public void Initialize()
        {
            // todo 初日のテキストをロードしてmessageに流す

            _screenEntity.OnNext(Screens.Menu);
        }
    }
}
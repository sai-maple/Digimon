using Digimon.Digimon.Scripts.Presentation.Presenter.Message;
using Digimon.Digimon.Scripts.Presentation.View.Message;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.Message
{
    public sealed class MessagePackage : LifetimeScope
    {
        [SerializeField] private MessageView messageView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MessagePresenter>();
            builder.RegisterComponent(messageView);
        }

        private void Reset()
        {
            messageView = GetComponent<MessageView>();
        }
    }
}
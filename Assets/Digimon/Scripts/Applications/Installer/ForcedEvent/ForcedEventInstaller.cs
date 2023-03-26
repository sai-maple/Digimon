using Digimon.Digimon.Scripts.Presentation.Presenter.ForcedEvent;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.ForcedEvent
{
    public sealed class ForcedEventInstaller : LifetimeScope
    {
        [SerializeField] private CanvasGroup _fadeMask;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ByDateEventInvoker>();
            builder.RegisterComponent(_fadeMask);
        }
    }
}
using Digimon.Digimon.Scripts.Presentation.Presenter.ForcedEvent;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.ForcedEvent
{
    public sealed class ForcedEventInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ByDateEventInvoker>();
        }
    }
}
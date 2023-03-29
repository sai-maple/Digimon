using Digimon.Digimon.Scripts.Applications.Static;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.Domain
{
    public sealed class PictureBookInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PictorialBookEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}
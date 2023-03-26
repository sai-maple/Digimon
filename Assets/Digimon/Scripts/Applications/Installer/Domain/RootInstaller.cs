using Digimon.Digimon.Scripts.Domain.Entity;
using Digimon.Digimon.Scripts.Domain.UseCase;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.Domain
{
    public sealed class RootInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<DateTimeEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<MonsterTypeEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<ScreenEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<StatusEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<MessageEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<MonsterAnimationEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<StaminaEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<BattleEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<AudioEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            builder.Register<CommandUseCase>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<TrainingUseCase>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<BattleUseCase>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}
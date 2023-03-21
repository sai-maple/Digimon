using Digimon.Digimon.Scripts.Presentation.Presenter.Monster;
using Digimon.Digimon.Scripts.Presentation.View.Monster;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.Monster
{
    [RequireComponent(typeof(MonsterAnimationView))]
    public sealed class MonsterPackage : LifetimeScope
    {
        [SerializeField] private MonsterAnimationView _animationView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MonsterAnimationPresenter>();
            builder.RegisterComponent(_animationView);
        }

        private void Reset()
        {
            _animationView = GetComponent<MonsterAnimationView>();
        }
    }
}
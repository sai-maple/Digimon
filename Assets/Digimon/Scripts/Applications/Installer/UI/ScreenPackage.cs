using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Presentation.Presenter.UI;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.UI
{
    [RequireComponent(typeof(ScreenView))]
    public sealed class ScreenPackage : LifetimeScope
    {
        [SerializeField] private ScreenView _screenView;
        [SerializeField] private Screens _screen;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ScreenPresenter>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf()
                .WithParameter(_screen);
            builder.RegisterComponent(_screenView);
        }

        private void Reset()
        {
            _screenView = GetComponent<ScreenView>();
        }
    }
}
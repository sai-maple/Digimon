using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Presentation.Presenter.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.UI
{
    [RequireComponent(typeof(Button))]
    public sealed class ScreenButtonPackage : LifetimeScope
    {
        [SerializeField] private Button _button;
        [SerializeField] private Screens _screen;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ScreenButtonPresenter>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf()
                .WithParameter(_screen);
            builder.RegisterComponent(_button);
        }

        private void Reset()
        {
            _button = GetComponent<Button>();
        }
    }
}
using Digimon.Digimon.Scripts.Presentation.Presenter.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.UI
{
    public sealed class BackgroundPackage : LifetimeScope
    {
        [SerializeField] private Image _image;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BackgroundColorPresenter>();
            builder.RegisterComponent(_image);
        }
    }
}
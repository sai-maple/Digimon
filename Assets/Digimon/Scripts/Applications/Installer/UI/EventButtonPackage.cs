using Digimon.Digimon.Scripts.Presentation.Presenter.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.UI
{
    [RequireComponent(typeof(Button))]
    public sealed class EventButtonPackage : LifetimeScope
    {
        [SerializeField] private Button _button;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<EventButtonPresenter>();
            builder.RegisterComponent(_button);
        }

        private void Reset()
        {
            _button = GetComponent<Button>();
        }
    }
}
using Digimon.Digimon.Scripts.Presentation.Presenter.UI;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.UI
{
    [RequireComponent(typeof(CommonView))]
    public sealed class CommonViewPackage : LifetimeScope
    {
        [SerializeField] private CommonView _commonView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<CommonViewPresenter>();
            builder.RegisterComponent(_commonView);
        }

        private void Reset()
        {
            _commonView = GetComponent<CommonView>();
        }
    }
}
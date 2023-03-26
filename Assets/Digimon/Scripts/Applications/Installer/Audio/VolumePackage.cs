using Digimon.Digimon.Scripts.Presentation.Presenter.Audio;
using Digimon.Digimon.Scripts.Presentation.View.Audio;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.Audio
{
    [RequireComponent(typeof(VolumeView))]
    public sealed class VolumePackage : LifetimeScope
    {
        [SerializeField] private VolumeView _volumeView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<VolumePresenter>();
            builder.RegisterComponent(_volumeView);
        }

        private void Reset()
        {
            _volumeView = GetComponent<VolumeView>();
        }
    }
}
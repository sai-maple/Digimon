using Digimon.Digimon.Scripts.Presentation.Presenter.Audio;
using Digimon.Digimon.Scripts.Presentation.View.Audio;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.Audio
{
    [RequireComponent(typeof(AudioView))]
    public sealed class AudioPackage : LifetimeScope
    {
        [SerializeField] private AudioView _audioView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<AudioPresenter>();
            builder.RegisterComponent(_audioView);
        }

        private void Reset()
        {
            _audioView = GetComponent<AudioView>();
        }
    }
}
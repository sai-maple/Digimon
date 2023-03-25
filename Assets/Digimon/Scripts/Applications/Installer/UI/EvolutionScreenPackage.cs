using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Presentation.Presenter.UI;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.UI
{
    [RequireComponent(typeof(ScreenView), typeof(EvolutionView))]
    public sealed class EvolutionScreenPackage : LifetimeScope
    {
        [SerializeField] private ScreenView _screenView;
        [SerializeField] private EvolutionView _evolutionView;
        [SerializeField] private Screens screen;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<EvolutionScreenPresenter>().WithParameter(screen);
            builder.RegisterComponent(_screenView);
            builder.RegisterComponent(_evolutionView);
        }

        private void Reset()
        {
            _screenView = GetComponent<ScreenView>();
            _evolutionView = GetComponent<EvolutionView>();
        }
    }
}
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Presentation.Presenter.UI;
using Digimon.Digimon.Scripts.Presentation.View.Monster;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.UI
{
    [RequireComponent(typeof(ScreenView), typeof(EvolutionView))]
    public sealed class LoseScreenPackage: LifetimeScope
    {
        [SerializeField] private ScreenView _screenView;
        [SerializeField] private EvolutionView _evolutionView;
        [SerializeField] private MonsterSpawner _monsterSpawner;
        [SerializeField] private Screens _screen;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LoseScreenPresenter>().WithParameter(_screen);
            builder.RegisterComponent(_screenView);
            builder.RegisterComponent(_evolutionView);
            builder.RegisterComponent(_monsterSpawner);
        }

        private void Reset()
        {
            _screenView = GetComponent<ScreenView>();
            _evolutionView = GetComponent<EvolutionView>();
        }
    }
}
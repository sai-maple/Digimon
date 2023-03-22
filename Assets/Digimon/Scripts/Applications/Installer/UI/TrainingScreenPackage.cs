using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Presentation.Presenter.UI;
using Digimon.Digimon.Scripts.Presentation.View.Monster;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UnityEngine;
using UnityEngine.Playables;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.UI
{
    public sealed class TrainingScreenPackage : LifetimeScope
    {
        [SerializeField] private ScreenView screenView;
        [SerializeField] private MonsterSpawner monsterSpawner;
        [SerializeField] private PlayableDirector playableDirector;
        [SerializeField] private Screens screen;
        [SerializeField] private TrainingType trainingType;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<TrainingScreenPresenter>().WithParameter(screen).WithParameter(trainingType);
            builder.RegisterComponent(screenView);
            builder.RegisterComponent(monsterSpawner);
            builder.RegisterComponent(playableDirector);
        }

        private void Reset()
        {
            screenView = GetComponent<ScreenView>();
        }
    }
}
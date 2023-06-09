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
    [RequireComponent(typeof(ScreenView))]
    public sealed class TrainingScreenPackage : LifetimeScope
    {
        [SerializeField] private ScreenView _screenView;
        [SerializeField] private MonsterSpawner _monsterSpawner;
        [SerializeField] private PlayableDirector _playableDirector;
        [SerializeField] private Screens _screen;
        [SerializeField] private TrainingType _trainingType;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<TrainingScreenPresenter>().WithParameter(_screen).WithParameter(_trainingType);
            builder.RegisterComponent(_screenView);
            builder.RegisterComponent(_monsterSpawner);
            builder.RegisterComponent(_playableDirector);
        }

        private void Reset()
        {
            _screenView = GetComponent<ScreenView>();
        }
    }
}
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Presentation.Presenter.UI;
using Digimon.Digimon.Scripts.Presentation.View.Monster;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using EventType = Digimon.Digimon.Scripts.Applications.Enums.EventType;

namespace Digimon.Digimon.Scripts.Applications.Installer.UI
{
    [RequireComponent(typeof(ScreenView))]
    public sealed class EventScreenPackage : LifetimeScope
    {
        [SerializeField] private ScreenView _screenView;
        [SerializeField] private MonsterSpawner _monsterSpawner;
        [SerializeField] private Screens screen;
        [SerializeField] private EventType _eventType;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<EventScreenPresenter>().WithParameter(screen).WithParameter(_eventType);
            builder.RegisterComponent(_screenView);
            builder.RegisterComponent(_monsterSpawner);
        }

        private void Reset()
        {
            _screenView = GetComponent<ScreenView>();
        }
    }
}
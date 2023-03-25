using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Presentation.Presenter.UI;
using Digimon.Digimon.Scripts.Presentation.View.Monster;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.UI
{
    [RequireComponent(typeof(ScreenView))]
    public sealed class WinScreenPackage : LifetimeScope
    {
        [SerializeField] private ScreenView _screenView;
        [SerializeField] private ConfirmView _confirmView;
        [SerializeField] private WinScreenView _winScreenView;
        [SerializeField] private MonsterSpawner _monsterSpawner;
        [SerializeField] private Screens _screen;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BattleWinScreenPresenter>().WithParameter(_screen);
            builder.RegisterComponent(_screenView);
            builder.RegisterComponent(_monsterSpawner);
            builder.RegisterComponent(_confirmView);
            builder.RegisterComponent(_winScreenView);
        }

        private void Reset()
        {
            _screenView = GetComponent<ScreenView>();
            _winScreenView = GetComponent<WinScreenView>();
        }
    }
}
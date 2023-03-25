using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Presentation.Presenter.UI;
using Digimon.Digimon.Scripts.Presentation.View.Battle;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.UI
{
    [RequireComponent(typeof(ScreenView), typeof(BattleView))]
    public sealed class BattleScreenPackage : LifetimeScope
    {
        [SerializeField] private BattleView _battleView;
        [SerializeField] private ScreenView _screenView;
        [SerializeField] private Screens _screen;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<TrainingScreenPresenter>().WithParameter(_screen);
            builder.RegisterComponent(_screenView);
            builder.RegisterComponent(_battleView);
        }

        private void Reset()
        {
            _screenView = GetComponent<ScreenView>();
            _battleView = GetComponent<BattleView>();
        }
    }
}
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Presentation.Presenter.UI;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.UI
{
    [RequireComponent(typeof(ScreenView), typeof(TrainingListView))]
    public sealed class TrainingListPackage : LifetimeScope
    {
        [SerializeField] private ScreenView _screenView;
        [SerializeField] private TrainingListView _trainingListView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<TrainingListScreenPresenter>().WithParameter(Screens.TrainingList);
            builder.RegisterComponent(_screenView);
            builder.RegisterComponent(_trainingListView);
        }

        private void Reset()
        {
            _screenView = GetComponent<ScreenView>();
            _trainingListView = GetComponent<TrainingListView>();
        }
    }
}
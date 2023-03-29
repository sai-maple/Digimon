using Digimon.Digimon.Scripts.Presentation.Presenter.PictureBook;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.UI
{
    public sealed class PictureBookPackage : LifetimeScope
    {
        [SerializeField] private FadeView _fadeView;
        [SerializeField] private Transform _content;
        [SerializeField] private Button _returnButton;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PictureBookPresenter>();
            builder.RegisterComponent(_fadeView);
            builder.RegisterComponent(_content);
            builder.RegisterInstance(_returnButton);
        }
    }
}
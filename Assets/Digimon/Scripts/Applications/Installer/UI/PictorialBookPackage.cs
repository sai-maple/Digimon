using Digimon.Digimon.Scripts.Presentation.Presenter.PictorialBook;
using Digimon.Digimon.Scripts.Presentation.View.PictorialBook;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Applications.Installer.UI
{
    public sealed class PictorialBookPackage : LifetimeScope
    {
        [SerializeField] private FadeView _fadeView;
        [SerializeField] private Transform _content;
        [SerializeField] private Button _returnButton;
        [SerializeField] private PictorialBookView _prefab;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PictorialBookPresenter>();
            builder.RegisterComponent(_fadeView);
            builder.RegisterComponent(_content);
            builder.RegisterComponent(_returnButton);
            builder.RegisterInstance(_prefab);
        }
    }
}
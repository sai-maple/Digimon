using System;
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Domain.Entity;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UniRx;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.UI
{
    public sealed class ScreenPresenter : IInitializable, IDisposable
    {
        private readonly ScreenEntity _screenEntity;
        private readonly ScreenBase _screenBase;
        private readonly Screens _screens;

        private readonly CompositeDisposable _disposable = new();

        public ScreenPresenter(ScreenEntity screenEntity, ScreenBase screenBase, Screens screens)
        {
            _screenEntity = screenEntity;
            _screenBase = screenBase;
            _screens = screens;
        }

        public void Initialize()
        {
            _screenEntity.OnChangedAsObservable()
                .Subscribe(screen =>
                {
                    if (screen == _screens)
                    {
                        _screenBase.PresentAsync().Forget();
                    }
                    else
                    {
                        _screenBase.DismissAsync().Forget();
                    }
                }).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
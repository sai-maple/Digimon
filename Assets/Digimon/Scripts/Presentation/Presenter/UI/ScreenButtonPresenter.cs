using System;
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Domain.Entity;
using UniRx;
using UnityEngine.UI;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.UI
{
    public sealed class ScreenButtonPresenter : IInitializable, IDisposable
    {
        private readonly ScreenService _screenService;
        private readonly Button _button;
        private readonly Screens _screens;

        private readonly CompositeDisposable _disposable = new();

        public ScreenButtonPresenter(ScreenService screenService, Button button, Screens screens)
        {
            _screenService = screenService;
            _button = button;
            _screens = screens;
        }

        public void Initialize()
        {
            _button.OnClickAsObservable()
                .Subscribe(_ => _screenService.OnNext(_screens))
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
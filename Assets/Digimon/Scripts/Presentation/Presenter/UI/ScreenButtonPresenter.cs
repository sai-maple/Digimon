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
        private readonly DateTimeEntity _dateTimeEntity;
        private readonly ScreenEntity _screenEntity;
        private readonly Button _button;
        private readonly Screens _screens;

        private readonly CompositeDisposable _disposable = new();

        public ScreenButtonPresenter(DateTimeEntity dateTimeEntity, ScreenEntity screenEntity, Button button,
            Screens screens)
        {
            _dateTimeEntity = dateTimeEntity;
            _screenEntity = screenEntity;
            _button = button;
            _screens = screens;
        }

        public void Initialize()
        {
            _button.OnClickAsObservable()
                .Where(_ => _dateTimeEntity.GameTime != GameTime.Evening)
                .Subscribe(_ => _screenEntity.OnNext(_screens))
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
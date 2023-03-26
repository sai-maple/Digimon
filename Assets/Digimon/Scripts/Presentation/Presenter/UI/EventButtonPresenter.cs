using System;
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Domain.Entity;
using UniRx;
using UnityEngine.UI;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.UI
{
    public sealed class EventButtonPresenter: IInitializable, IDisposable
    {
        private readonly DateTimeEntity _dateTimeEntity;
        private readonly ScreenEntity _screenEntity;
        private readonly Button _button;

        private readonly CompositeDisposable _disposable = new();

        public EventButtonPresenter(DateTimeEntity dateTimeEntity, ScreenEntity screenEntity, Button button)
        {
            _dateTimeEntity = dateTimeEntity;
            _screenEntity = screenEntity;
            _button = button;
        }

        public void Initialize()
        {
            _button.OnClickAsObservable()
                .Where(_ => _dateTimeEntity.GameTime != GameTime.Evening)
                .Subscribe(_ => _screenEntity.Event())
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
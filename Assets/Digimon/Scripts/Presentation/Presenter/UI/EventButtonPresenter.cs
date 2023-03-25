using System;
using Digimon.Digimon.Scripts.Domain.Entity;
using UniRx;
using UnityEngine.UI;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.UI
{
    public sealed class EventButtonPresenter: IInitializable, IDisposable
    {
        private readonly ScreenEntity _screenEntity;
        private readonly Button _button;

        private readonly CompositeDisposable _disposable = new();

        public EventButtonPresenter(ScreenEntity screenEntity, Button button)
        {
            _screenEntity = screenEntity;
            _button = button;
        }

        public void Initialize()
        {
            _button.OnClickAsObservable()
                .Subscribe(_ => _screenEntity.Event())
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
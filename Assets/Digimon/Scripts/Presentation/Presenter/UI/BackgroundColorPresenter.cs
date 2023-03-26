using System;
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Domain.Entity;
using UniRx;
using UnityEngine.UI;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.UI
{
    public sealed class BackgroundColorPresenter : IInitializable, IDisposable
    {
        private readonly DateTimeEntity _dateTimeEntity;
        private readonly Image _image;

        private readonly CompositeDisposable _disposable = new();

        public BackgroundColorPresenter(DateTimeEntity dateTimeEntity, Image image)
        {
            _dateTimeEntity = dateTimeEntity;
            _image = image;
        }

        public void Initialize()
        {
            _dateTimeEntity.OnGameTimeChangedAsObservable()
                .Subscribe(gameTime => _image.color = gameTime.BackColor())
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
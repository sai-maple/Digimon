using System;
using Digimon.Digimon.Scripts.Domain.Entity;
using Digimon.Digimon.Scripts.Presentation.View.Audio;
using UniRx;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.Audio
{
    public sealed class VolumePresenter : IInitializable, IDisposable
    {
        private readonly AudioEntity _audioEntity;
        private readonly VolumeView _volumeView;
        private readonly CompositeDisposable _disposable = new();

        public VolumePresenter(AudioEntity audioEntity, VolumeView volumeView)
        {
            _audioEntity = audioEntity;
            _volumeView = volumeView;
        }

        public void Initialize()
        {
            _volumeView.Initialize(_audioEntity.VolumeBgm, _audioEntity.VolumeSe);
            _volumeView.OnBgmChangedAsObservable()
                .Subscribe(_audioEntity.OnChangeBgmVolume)
                .AddTo(_disposable);
            _volumeView.OnSeChangedAsObservable()
                .Subscribe(_audioEntity.OnChangeSeVolume)
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
using System;
using Digimon.Digimon.Scripts.Domain.Entity;
using Digimon.Digimon.Scripts.Presentation.View.Audio;
using UniRx;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.Audio
{
    public sealed class AudioPresenter : IInitializable, IDisposable
    {
        private readonly AudioEntity _audioEntity;
        private readonly AudioView _audioView;

        private readonly CompositeDisposable _disposable = new();

        public AudioPresenter(AudioEntity audioEntity, AudioView audioView)
        {
            _audioEntity = audioEntity;
            _audioView = audioView;
        }

        public void Initialize()
        {
            _audioEntity.OnBgmAsObservable()
                .Subscribe(_audioView.PlayBgm)
                .AddTo(_disposable);
            _audioEntity.OnSeAsObservable()
                .Subscribe(_audioView.PlaySe)
                .AddTo(_disposable);
            
            _audioEntity.OnBgmVolumeAsObservable()
                .Subscribe(_audioView.OnChangeBgmVolume)
                .AddTo(_disposable);
            _audioEntity.OnSeVolumeAsObservable()
                .Subscribe(_audioView.OnChangeSeVolume)
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
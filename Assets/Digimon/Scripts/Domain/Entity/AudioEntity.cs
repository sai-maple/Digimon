using System;
using Digimon.Digimon.Scripts.Applications.Enums;
using UniRx;
using UnityEngine;

namespace Digimon.Digimon.Scripts.Domain.Entity
{
    public sealed class AudioEntity : IDisposable
    {
        private readonly ReactiveProperty<Bgm> _bgm = new(Bgm.Main);
        private readonly Subject<Se> _se = new();

        private readonly ReactiveProperty<float> _volumeBgm = new(0.3f);
        private readonly ReactiveProperty<float> _volumeSe = new(0.5f);

        public float VolumeBgm => _volumeBgm.Value;
        public float VolumeSe => _volumeSe.Value;

        public IObservable<Bgm> OnBgmAsObservable()
        {
            return _bgm;
        }
        
        public IObservable<Se> OnSeAsObservable()
        {
            return _se.Share();
        }
        
        public IObservable<float> OnBgmVolumeAsObservable()
        {
            return _volumeBgm;
        }
        
        public IObservable<float> OnSeVolumeAsObservable()
        {
            return _volumeSe;
        }

        public void PlayBgm(Bgm bgm)
        {
            _bgm.Value = bgm;
        }

        public void PlayBgm(string parameter)
        {
            if (!Enum.TryParse(parameter, out Bgm bgm))
            {
                Debug.Log($"{parameter} can not parse to Bgm.");
                return;
            }

            _bgm.Value = bgm;
        }

        public void PlaySe(Se se)
        {
            _se.OnNext(se);
        }

        public void PlaySe(string parameter)
        {
            if (!Enum.TryParse(parameter, out Se se))
            {
                Debug.Log($"{parameter} can not parse to Se.");
                return;
            }

            _se.OnNext(se);
        }

        public void OnChangeBgmVolume(float value)
        {
            _volumeBgm.Value = value;
        }
        
        public void OnChangeSeVolume(float value)
        {
            _volumeSe.Value = value;
        }

        public void Dispose()
        {
            _bgm?.Dispose();
            _se?.Dispose();
            _volumeBgm?.Dispose();
            _volumeSe?.Dispose();
        }
    }
}
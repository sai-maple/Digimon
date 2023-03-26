using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Digimon.Digimon.Scripts.Presentation.View.Audio
{
    public sealed class VolumeView : MonoBehaviour
    {
        [SerializeField] private Slider _sliderBgm;
        [SerializeField] private Slider _sliderSe;

        public IObservable<float> OnBgmChangedAsObservable()
        {
            return _sliderBgm.onValueChanged.AsObservable();
        }
        
        public IObservable<float> OnSeChangedAsObservable()
        {
            return _sliderSe.onValueChanged.AsObservable();
        }

        public void Initialize(float bgm, float se)
        {
            _sliderBgm.value = bgm;
            _sliderSe.value = se;
        }
    }
}
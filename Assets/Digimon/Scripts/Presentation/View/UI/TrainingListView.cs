using System;
using Digimon.Digimon.Scripts.Applications.Enums;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Digimon.Digimon.Scripts.Presentation.View.UI
{
    public sealed class TrainingListView : MonoBehaviour
    {
        [SerializeField] private Button _hpButton;
        [SerializeField] private Button _atkButton;
        [SerializeField] private Button _defButton;
        [SerializeField] private Button _speedButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private CrashRateView _hpRate;
        [SerializeField] private CrashRateView _atkRate;
        [SerializeField] private CrashRateView _defRate;
        [SerializeField] private CrashRateView _speedRate;

        public void Initialize(int hpRate, int atkRate, int defRate, int speedRate)
        {
            _hpRate.Display(hpRate);
            _atkRate.Display(atkRate);
            _defRate.Display(defRate);
            _speedRate.Display(speedRate);
        }

        public IObservable<TrainingType> OnHpTrainingAsObservable()
        {
            return _hpButton.OnClickAsObservable().Select(_ => TrainingType.Hp);
        }
        
        public IObservable<TrainingType> OnAtkTrainingAsObservable()
        {
            return _atkButton.OnClickAsObservable().Select(_ => TrainingType.Atk);
        }
        
        public IObservable<TrainingType> OnDefTrainingAsObservable()
        {
            return _defButton.OnClickAsObservable().Select(_ => TrainingType.Def);
        }
        
        public IObservable<TrainingType> OnSpeedTrainingAsObservable()
        {
            return _speedButton.OnClickAsObservable().Select(_ => TrainingType.Speed);
        }
        
        public IObservable<Unit> OnBackAsObservable()
        {
            return _backButton.OnClickAsObservable();
        }
    }
}
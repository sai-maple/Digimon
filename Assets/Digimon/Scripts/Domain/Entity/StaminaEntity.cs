using System;
using Digimon.Digimon.Scripts.Applications.Enums;
using UniRx;
using UnityEngine;

namespace Digimon.Digimon.Scripts.Domain.Entity
{
    public sealed class StaminaEntity : IDisposable
    {
        private readonly ReactiveProperty<int> _stamina = new(100);

        public IObservable<int> OnValueChangedAsObservable()
        {
            return _stamina;
        }

        public void UseOrRecover(int value)
        {
            _stamina.Value = Mathf.Clamp(_stamina.Value + value, 0, 100);
        }

        public int ClashRate(TrainingType trainingType,int status)
        {
            var bonus = trainingType == TrainingType.Hp ? 300 : 100;
            var tough = _stamina.Value + (bonus - status);

            var rate = 100 - tough;
            rate = Mathf.Clamp(rate, 0, 100);
            return rate;
        }

        public void Dispose()
        {
            _stamina.Dispose();
        }
    }
}
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

        public int ClashRate(TrainingType trainingType, int status)
        {
            var standard = trainingType == TrainingType.Hp ? 150 : 50;
            var bonus = Mathf.Clamp(-1 * Mathf.Pow((status - standard) / 10f, 3), -20, 20);
            var tough = _stamina.Value + bonus;

            var rate = 50 - tough;

            rate = Mathf.Clamp(rate, 0, 100);
            return Mathf.FloorToInt(rate);
        }

        public void Dispose()
        {
            _stamina.Dispose();
        }
    }
}
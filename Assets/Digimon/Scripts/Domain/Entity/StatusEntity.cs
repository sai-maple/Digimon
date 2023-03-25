using System;
using UniRx;
using UnityEngine;

namespace Digimon.Digimon.Scripts.Domain.Entity
{
    public class StatusEntity : IDisposable
    {
        private readonly ReactiveProperty<int> _bonusHp = new();
        private readonly ReactiveProperty<int> _bonusAtk = new();
        private readonly ReactiveProperty<int> _bonusDef = new();
        private readonly ReactiveProperty<int> _bonusSpeed = new();
        private readonly ReactiveProperty<int> _skillLevel = new(1);

        public int Hp => 10 + _bonusHp.Value;
        public int Atk => 5 + _bonusAtk.Value;
        public int Def => 5 + _bonusDef.Value;
        public int Speed => 5 + _bonusSpeed.Value;
        public int SkillLevel => _skillLevel.Value;

        public IObservable<(int previous, int current)> OnHpChangedAsObservable()
        {
            return _bonusHp.Zip(_bonusHp.Skip(1), (previous, current) => (previous + 10, current + 10));
        }

        public IObservable<(int previous, int current)> OnAtkChangedAsObservable()
        {
            return _bonusAtk.Zip(_bonusAtk.Skip(1), (previous, current) => (previous + 5, current + 5));
        }

        public IObservable<(int previous, int current)> OnDefChangedAsObservable()
        {
            return _bonusDef.Zip(_bonusDef.Skip(1), (previous, current) => (previous + 5, current + 5));
        }

        public IObservable<(int previous, int current)> OnSpeedChangedAsObservable()
        {
            return _bonusSpeed.Zip(_bonusSpeed.Skip(1), (previous, current) => (previous + 5, current + 5));
        }

        public IObservable<(int previous, int current)> OnSkillLevelChangedAsObservable()
        {
            return _skillLevel.Zip(_skillLevel.Skip(1), (previous, current) => (previous, current));
        }

        public void Plus(int[] status)
        {
            Plus(status[0], status[1], status[2], status[3], status[4]);
        }
        
        public void Plus(int hp = 0, int atk = 0, int def = 0, int speed = 0, int skillLevel = 0)
        {
            _bonusHp.Value = Mathf.Max(_bonusHp.Value + hp, 0);
            _bonusAtk.Value = Mathf.Max(_bonusAtk.Value + atk, 0);
            _bonusDef.Value = Mathf.Max(_bonusDef.Value + def, 0);
            _bonusSpeed.Value = Mathf.Max(_bonusSpeed.Value + speed, 0);
            _skillLevel.Value = Mathf.Max(_skillLevel.Value + skillLevel, 0);
        }

        // 初日や対戦に敗北後、成長分を1/10に
        public void Lose()
        {
            _bonusHp.Value /= 5;
            _bonusAtk.Value /= 5;
            _bonusDef.Value /= 5;
            _bonusSpeed.Value /= 5;
        }

        public void Dispose()
        {
            _bonusHp?.Dispose();
            _bonusAtk?.Dispose();
            _bonusDef?.Dispose();
            _bonusSpeed?.Dispose();
        }
    }
}
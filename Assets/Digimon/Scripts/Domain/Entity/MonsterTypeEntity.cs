using System;
using Digimon.Digimon.Scripts.Applications.Enums;
using UniRx;

namespace Digimon.Digimon.Scripts.Domain.Entity
{
    public sealed class MonsterTypeEntity : IDisposable
    {
        private ReactiveProperty<MonsterName> _monster;
        public MonsterName Value => _monster.Value;

        public IObservable<(MonsterName previous, MonsterName current)> OnEvolutionAsObservable()
        {
            return _monster.Zip(_monster.Skip(1), (previous, current) => (previous, current));
        }

        public void Evolution(int hp = 0, int atk = 0, int def = 0, int speed = 0)
        {
            // パラメータによって形態変化
            _monster.Value = _monster.Value.Evolution(hp, atk, def, speed);
        }

        public void Lose()
        {
            _monster.Value = MonsterName.Baby;
        }

        public void Dispose()
        {
            _monster?.Dispose();
        }
    }
}
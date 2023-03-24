using System;
using Digimon.Digimon.Scripts.Applications.Enums;
using UniRx;

namespace Digimon.Digimon.Scripts.Domain.Entity
{
    public sealed class MonsterTypeEntity : IDisposable
    {
        private readonly Subject<MonsterName> _monster = new();
        public MonsterName Value { get; private set; } = MonsterName.Baby;

        public IObservable<MonsterName> OnEvolutionAsObservable()
        {
            return _monster.Share();
        }

        public void Evolution(int hp = 0, int atk = 0, int def = 0, int speed = 0)
        {
            // パラメータによって形態変化
            Value = Value.Evolution(hp, atk, def, speed);
            _monster.OnNext(Value);
        }

        public void Lose()
        {
            Value = MonsterName.Baby;
            _monster.OnNext(Value);
        }

        public void Dispose()
        {
            _monster?.Dispose();
        }
    }
}
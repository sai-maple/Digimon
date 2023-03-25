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

        public string EvolutionFile(bool isBefore)
        {
            var fileName = "Events/Events/ForcedEvent/Evolution";
            var suffix = isBefore ? "" : "After";
            var index = "";
            switch (Value)
            {
                case MonsterName.Baby:
                    fileName = $"{fileName}{suffix}1";
                    break;
                case MonsterName.ChildNormal01:
                case MonsterName.ChildNormal02:
                case MonsterName.ChildRed01:
                case MonsterName.ChildRed02:
                case MonsterName.ChildGreen01:
                case MonsterName.ChildGreen02:
                case MonsterName.ChildBlue01:
                case MonsterName.ChildBlue02:
                case MonsterName.ChildYellow01:
                case MonsterName.ChildYellow02:
                    index = isBefore ? "2" : "1";
                    fileName = $"{fileName}{suffix}{index}";
                    break;
                case MonsterName.YouthNormal01:
                case MonsterName.YouthNormal02:
                case MonsterName.YouthRed01:
                case MonsterName.YouthRed02:
                case MonsterName.YouthGreen01:
                case MonsterName.YouthGreen02:
                case MonsterName.YouthBlue01:
                case MonsterName.YouthBlue02:
                case MonsterName.YouthYellow01:
                case MonsterName.YouthYellow02:
                    index = isBefore ? "3" : "2";
                    fileName = $"{fileName}{suffix}{index}";
                    break;
                case MonsterName.AdultNormal01:
                case MonsterName.AdultNormal02:
                case MonsterName.AdultRed01:
                case MonsterName.AdultRed02:
                case MonsterName.AdultGreen01:
                case MonsterName.AdultGreen02:
                case MonsterName.AdultBlue01:
                case MonsterName.AdultBlue02:
                case MonsterName.AdultYellow01:
                case MonsterName.AdultYellow02:
                    fileName = $"{fileName}{suffix}3";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return fileName;
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
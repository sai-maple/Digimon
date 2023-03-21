using Digimon.Digimon.Scripts.Applications.Enums;
using UniRx;

namespace Digimon.Digimon.Scripts.Domain.Entity
{
    public sealed class MonsterTypeEntity
    {
        private ReactiveProperty<MonsterName> _monster;

        public void Evolution(int hp = 0, int atk = 0, int def = 0, int speed = 0)
        {
            // パラメータによって形態変化
        }
    }
}
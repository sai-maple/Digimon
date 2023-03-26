using System.Linq;
using UnityEngine;

namespace Digimon.Digimon.Scripts.Applications.Enums
{
    public enum EvolutionType
    {
        HpAtk,
        HpDef,
        HpSpeed,
        HpExtra,

        AtkHp,
        AtkDef,
        AtkSpeed,
        AtkExtra,

        DefHp,
        DefAtk,
        DefSpeed,
        DefExtra,

        SpeedHp,
        SpeedAtk,
        SpeedDef,
        SpeedExtra,
    }

    public static class EvolutionTypeExtension
    {
        public static EvolutionType Evolution(int hp = 0, int atk = 0, int def = 0, int speed = 0)
        {
             // hpは通常の倍以上の数値があるので、Sランク基準で1/2計算する
            hp /= 2;
            if (IsMax(hp, atk, def, speed))
            {
                if (IsSimilar(atk, def, speed)) return EvolutionType.HpExtra;
                if (IsMax(atk, def, speed)) return EvolutionType.HpAtk;
                if (IsMax(def, atk, speed)) return EvolutionType.HpDef;
                return EvolutionType.HpSpeed;
            }
            else if (IsMax(atk, hp, def, speed))
            {
                if (IsSimilar(hp, def, speed)) return EvolutionType.AtkExtra;
                if (IsMax(hp, def, speed)) return EvolutionType.AtkHp;
                if (IsMax(def, hp, speed)) return EvolutionType.AtkDef;
                return EvolutionType.AtkSpeed;
            }
            else if (IsMax(def, hp, atk, speed))
            {
                if (IsSimilar(hp, atk, speed)) return EvolutionType.DefExtra;
                if (IsMax(hp, atk, speed)) return EvolutionType.DefHp;
                if (IsMax(atk, hp, speed)) return EvolutionType.DefAtk;
                return EvolutionType.DefSpeed;
            }
            else
            {
                if (IsSimilar(hp, atk, def)) return EvolutionType.SpeedExtra;
                if (IsMax(hp, atk, def)) return EvolutionType.SpeedHp;
                if (IsMax(def, hp, atk)) return EvolutionType.SpeedAtk;
                return EvolutionType.SpeedDef;
            }
        }

        private static bool IsMax(int self, params int[] others)
        {
            return others.All(other => other <= self);
        }

        private static bool IsSimilar(params int[] others)
        {
            var min = Mathf.Min(others);
            var max = Mathf.Max(others);
            return max - min < 5;
        }
    }
}
using System;
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

        None,
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

        public static EvolutionType GetType(this MonsterName self)
        {
            return self switch
            {
                MonsterName.Baby => EvolutionType.None,
                MonsterName.ChildNormal01 => EvolutionType.HpAtk,
                MonsterName.ChildNormal02 => EvolutionType.HpDef,
                MonsterName.ChildRed01 => EvolutionType.AtkHp,
                MonsterName.ChildRed02 => EvolutionType.AtkDef,
                MonsterName.ChildGreen01 => EvolutionType.DefHp,
                MonsterName.ChildGreen02 => EvolutionType.DefAtk,
                MonsterName.ChildBlue01 => EvolutionType.SpeedHp,
                MonsterName.ChildBlue02 => EvolutionType.SpeedAtk,
                MonsterName.ChildYellow01 => EvolutionType.HpExtra,
                MonsterName.ChildYellow02 => EvolutionType.SpeedExtra,

                MonsterName.YouthNormal01 => EvolutionType.HpAtk,
                MonsterName.YouthNormal02 => EvolutionType.HpDef,
                MonsterName.YouthRed01 => EvolutionType.AtkHp,
                MonsterName.YouthRed02 => EvolutionType.AtkDef,
                MonsterName.YouthGreen01 => EvolutionType.DefHp,
                MonsterName.YouthGreen02 => EvolutionType.DefAtk,
                MonsterName.YouthBlue01 => EvolutionType.SpeedHp,
                MonsterName.YouthBlue02 => EvolutionType.SpeedAtk,
                MonsterName.YouthYellow01 => EvolutionType.HpExtra,
                MonsterName.YouthYellow02 => EvolutionType.SpeedExtra,

                MonsterName.AdultNormal01 => EvolutionType.HpAtk,
                MonsterName.AdultNormal02 => EvolutionType.HpDef,
                MonsterName.AdultRed01 => EvolutionType.AtkHp,
                MonsterName.AdultRed02 => EvolutionType.AtkDef,
                MonsterName.AdultGreen01 => EvolutionType.DefHp,
                MonsterName.AdultGreen02 => EvolutionType.DefAtk,
                MonsterName.AdultBlue01 => EvolutionType.SpeedHp,
                MonsterName.AdultBlue02 => EvolutionType.SpeedAtk,
                MonsterName.AdultYellow01 => EvolutionType.HpExtra,
                MonsterName.AdultYellow02 => EvolutionType.SpeedExtra,
                _ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
            };
        }

        public static string GetLabel(this EvolutionType evolutionType)
        {
            //     <color=#F27405>たいりょく</color>
            //     <color=#F2360C>こうげき</color>
            //     <color=#84BF2A>ぼうぎょ</color>
            //     <color=#44AFF2>すばやさ</color>
            //     <color=#7756BF>バランス</color>

            return evolutionType switch
            {
                EvolutionType.HpAtk => "<<color=#F27405>たいりょく</color>,<color=#F2360C>こうげき</color>>",
                EvolutionType.HpDef => "<<color=#F27405>たいりょく</color>,<color=#84BF2A>ぼうぎょ</color>>",
                EvolutionType.HpSpeed => "<<color=#F27405>たいりょく</color>,<color=#44AFF2>すばやさ</color>>",
                EvolutionType.HpExtra => "<<color=#F27405>たいりょく</color>,<color=#7756BF>バランス</color>>",
                EvolutionType.AtkHp => "<<color=#F2360C>こうげき</color>,<color=#F27405>たいりょく</color>>",
                EvolutionType.AtkDef => "<<color=#F2360C>こうげき</color>,<color=#84BF2A>ぼうぎょ</color>>",
                EvolutionType.AtkSpeed => "<<color=#F2360C>こうげき</color>,<color=#44AFF2>すばやさ</color>>",
                EvolutionType.AtkExtra => "<<color=#F2360C>こうげき</color>,<color=#7756BF>バランス</color>>",
                EvolutionType.DefHp => "<<color=#84BF2A>ぼうぎょ</color>,<color=#F27405>たいりょく</color>>",
                EvolutionType.DefAtk => "<<color=#84BF2A>ぼうぎょ</color>,<color=#F2360C>こうげき</color>>",
                EvolutionType.DefSpeed => "<<color=#84BF2A>ぼうぎょ</color>,<color=#44AFF2>すばやさ</color>>",
                EvolutionType.DefExtra => "<<color=#84BF2A>ぼうぎょ</color>,<color=#7756BF>バランス</color>>",
                EvolutionType.SpeedHp => "<<color=#44AFF2>すばやさ</color>,<color=#F27405>たいりょく</color>",
                EvolutionType.SpeedAtk => "<<color=#44AFF2>すばやさ</color>,<color=#F2360C>こうげき</color>>",
                EvolutionType.SpeedDef => "<<color=#44AFF2>すばやさ</color>,<color=#84BF2A>ぼうぎょ</color>>",
                EvolutionType.SpeedExtra => "<<color=#44AFF2>すばやさ</color>,<color=#7756BF>バランス</color>>",
                EvolutionType.None => "うまれたての姿",
                _ => throw new ArgumentOutOfRangeException(nameof(evolutionType), evolutionType, null)
            };
        }

        private static bool IsSimilar(params int[] others)
        {
            var min = Mathf.Min(others);
            var max = Mathf.Max(others);
            return max - min < 5;
        }
    }
}
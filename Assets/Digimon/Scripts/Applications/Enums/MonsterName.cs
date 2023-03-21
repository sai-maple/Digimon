using System;

namespace Digimon.Digimon.Scripts.Applications.Enums
{
    public enum MonsterName
    {
        Baby,

        ChildNormal01,
        ChildNormal02,
        ChildRed01,
        ChildRed02,
        ChildGreen01,
        ChildGreen02,
        ChildBlue01,
        ChildBlue02,
        ChildYellow01,
        ChildYellow02,

        YouthNormal01,
        YouthNormal02,
        YouthRed01,
        YouthRed02,
        YouthGreen01,
        YouthGreen02,
        YouthBlue01,
        YouthBlue02,
        YouthYellow01,
        YouthYellow02,

        AdultNormal01,
        AdultNormal02,
        AdultRed01,
        AdultRed02,
        AdultGreen01,
        AdultGreen02,
        AdultBlue01,
        AdultBlue02,
        AdultYellow01,
        AdultYellow02,
    }

    public static class MonsterNameExtension
    {
        // Normal : Hp系, Red : Atk系, Green : Def系, Blue : Speed系
        public static MonsterName Evolution(this MonsterName self, int hp = 0, int atk = 0, int def = 0, int speed = 0)
        {
            switch (self)
            {
                case MonsterName.Baby:
                    return EvolutionTypeExtension.Evolution(hp, atk, def, speed) switch
                    {
                        EvolutionType.HpAtk => MonsterName.ChildNormal01,
                        EvolutionType.HpDef => MonsterName.ChildNormal02,
                        EvolutionType.HpSpeed => MonsterName.ChildNormal01,
                        EvolutionType.HpExtra => MonsterName.ChildYellow01,
                        EvolutionType.AtkHp => MonsterName.ChildRed01,
                        EvolutionType.AtkDef => MonsterName.ChildRed02,
                        EvolutionType.AtkSpeed => MonsterName.ChildRed01,
                        EvolutionType.AtkExtra => MonsterName.ChildYellow02,
                        EvolutionType.DefHp => MonsterName.ChildGreen01,
                        EvolutionType.DefAtk => MonsterName.ChildGreen02,
                        EvolutionType.DefSpeed => MonsterName.ChildGreen01,
                        EvolutionType.DefExtra => MonsterName.ChildYellow01,
                        EvolutionType.SpeedHp => MonsterName.ChildBlue01,
                        EvolutionType.SpeedAtk => MonsterName.ChildBlue02,
                        EvolutionType.SpeedDef => MonsterName.ChildBlue01,
                        EvolutionType.SpeedExtra => MonsterName.ChildYellow02,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                // 幼年期
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
                    return EvolutionTypeExtension.Evolution(hp, atk, def, speed) switch
                    {
                        EvolutionType.HpAtk => MonsterName.YouthNormal01,
                        EvolutionType.HpDef => MonsterName.YouthNormal02,
                        EvolutionType.HpSpeed => MonsterName.YouthNormal01,
                        EvolutionType.HpExtra => MonsterName.YouthYellow01,
                        EvolutionType.AtkHp => MonsterName.YouthRed01,
                        EvolutionType.AtkDef => MonsterName.YouthRed02,
                        EvolutionType.AtkSpeed => MonsterName.YouthRed01,
                        EvolutionType.AtkExtra => MonsterName.YouthYellow02,
                        EvolutionType.DefHp => MonsterName.YouthGreen01,
                        EvolutionType.DefAtk => MonsterName.YouthGreen02,
                        EvolutionType.DefSpeed => MonsterName.YouthGreen01,
                        EvolutionType.DefExtra => MonsterName.YouthYellow01,
                        EvolutionType.SpeedHp => MonsterName.YouthBlue01,
                        EvolutionType.SpeedAtk => MonsterName.YouthBlue02,
                        EvolutionType.SpeedDef => MonsterName.YouthBlue01,
                        EvolutionType.SpeedExtra => MonsterName.YouthYellow02,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                // 青年期
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
                    return EvolutionTypeExtension.Evolution(hp, atk, def, speed) switch
                    {
                        EvolutionType.HpAtk => MonsterName.AdultNormal01,
                        EvolutionType.HpDef => MonsterName.AdultNormal02,
                        EvolutionType.HpSpeed => MonsterName.AdultNormal01,
                        EvolutionType.HpExtra => MonsterName.AdultYellow01,
                        EvolutionType.AtkHp => MonsterName.AdultRed01,
                        EvolutionType.AtkDef => MonsterName.AdultRed02,
                        EvolutionType.AtkSpeed => MonsterName.AdultRed01,
                        EvolutionType.AtkExtra => MonsterName.AdultYellow02,
                        EvolutionType.DefHp => MonsterName.AdultGreen01,
                        EvolutionType.DefAtk => MonsterName.AdultGreen02,
                        EvolutionType.DefSpeed => MonsterName.AdultGreen01,
                        EvolutionType.DefExtra => MonsterName.AdultYellow01,
                        EvolutionType.SpeedHp => MonsterName.AdultBlue01,
                        EvolutionType.SpeedAtk => MonsterName.AdultBlue02,
                        EvolutionType.SpeedDef => MonsterName.AdultBlue01,
                        EvolutionType.SpeedExtra => MonsterName.AdultYellow02,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                // 完全体
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
using System;
using UnityEngine;

namespace Digimon.Digimon.Scripts.Applications.Enums
{
    public enum StatusRank
    {
        G,
        F,
        E,
        D,
        C,
        B,
        A,
        S
    }

    public static class StatusRankExtension
    {
        public static string Label(this StatusRank self)
        {
            return self switch
            {
                StatusRank.G => "Ｇ",
                StatusRank.F => "Ｆ",
                StatusRank.E => "Ｅ",
                StatusRank.D => "Ｄ",
                StatusRank.C => "Ｃ",
                StatusRank.B => "Ｂ",
                StatusRank.A => "Ａ",
                StatusRank.S => "Ｓ",
                _ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
            };
        }

        public static Color Color(this StatusRank self)
        {
            return self switch
            {
                StatusRank.G => new Color(0.26f, 0.26f, 0.26f, 0.43f),
                StatusRank.F => new Color(0.27f, 0.44f, 1f),
                StatusRank.E => new Color(0.29f, 1f, 0.32f),
                StatusRank.D => new Color(0.99f, 1f, 0.24f),
                StatusRank.C => new Color(1f, 0.79f, 0.25f),
                StatusRank.B => new Color(1f, 0.28f, 0.2f),
                StatusRank.A => new Color(1f, 0.28f, 0.91f),
                StatusRank.S => new Color(0.95f, 0.23f, 1f),
                _ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
            };
        }
    }
}
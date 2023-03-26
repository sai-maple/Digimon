using System;
using UnityEngine;

namespace Digimon.Digimon.Scripts.Applications.Enums
{
    public enum GameTime
    {
        Morning,
        Afternoon,
        Evening
    }

    public static class GameTimeExtension
    {
        public static GameTime Next(this GameTime self)
        {
            return self switch
            {
                GameTime.Morning => GameTime.Afternoon,
                GameTime.Afternoon => GameTime.Evening,
                GameTime.Evening => GameTime.Morning,
                _ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
            };
        }
        
        public static string Label(this GameTime self)
        {
            return self switch
            {
                GameTime.Morning => "あさ",
                GameTime.Afternoon => "ひる",
                GameTime.Evening => "よる",
                _ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
            };
        }
        
        public static Color Color(this GameTime self)
        {
            return self switch
            {
                GameTime.Morning => new Color(1f, 0.75f, 0f),
                GameTime.Afternoon => new Color(0.59f, 1f, 0.08f),
                GameTime.Evening => new Color(0.15f, 0.41f, 1f),
                _ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
            };
        }
        
        public static Color BackColor(this GameTime self)
        {
            return self switch
            {
                GameTime.Morning => new Color(1f, 0.96f, 0.87f),
                GameTime.Afternoon => UnityEngine.Color.white,
                GameTime.Evening => new Color(0.47f, 0.51f, 1f),
                _ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
            };
        }
    }
}
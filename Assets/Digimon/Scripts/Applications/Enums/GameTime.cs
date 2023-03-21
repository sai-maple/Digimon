using System;

namespace Digimon.Digimon.Scripts.Applications.Enums
{
    public enum GameTime
    {
        Morning,
        Afternoon,
    }

    public static class GameTimeExtension
    {
        public static GameTime Next(this GameTime self)
        {
            return self switch
            {
                GameTime.Morning => GameTime.Afternoon,
                GameTime.Afternoon => GameTime.Morning,
                _ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
            };
        }
    }
}
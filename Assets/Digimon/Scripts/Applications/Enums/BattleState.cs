using System;

namespace Digimon.Digimon.Scripts.Applications.Enums
{
    public enum BattleState
    {
        Intro1, // いよいよ
        Intro2, // 敵登場後 + セリフ
        BattleStart, // 開始帯的な
        MyTurn,
        EnemyTurn,
        Win, // 画面表示
        Win2, // UI表示
        Lose, // 敗北
        Lose2, // 退化演出
    }

    public static class BattleStateExtension
    {
        public static BattleState Next(this BattleState self)
        {
            return self switch
            {
                BattleState.Intro1 => BattleState.Intro2,
                BattleState.Intro2 => BattleState.BattleStart,
                BattleState.BattleStart => BattleState.BattleStart,
                BattleState.MyTurn => BattleState.MyTurn,
                BattleState.EnemyTurn => BattleState.EnemyTurn,
                BattleState.Win => BattleState.Win2,
                BattleState.Lose => BattleState.Lose2,
                _ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
            };
        }
    }
}
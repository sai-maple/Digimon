using System;
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Domain.Entity;

namespace Digimon.Digimon.Scripts.Domain.UseCase
{
    public sealed class BattleUseCase
    {
        private readonly BattleEntity _battleEntity;
        private readonly StatusEntity _statusEntity;
        private readonly MessageEntity _messageEntity;
        private readonly ScreenEntity _screenEntity;

        public BattleUseCase(BattleEntity battleEntity, StatusEntity statusEntity, MessageEntity messageEntity,
            ScreenEntity screenEntity)
        {
            _battleEntity = battleEntity;
            _statusEntity = statusEntity;
            _messageEntity = messageEntity;
            _screenEntity = screenEntity;
        }

        public (int selfHp, int enemyHp) Initialize()
        {
            _battleEntity.OnPresent(_statusEntity.Hp, _statusEntity.Atk, _statusEntity.Def, _statusEntity.Speed,
                _statusEntity.SkillLevel);
            return (_statusEntity.Hp, _battleEntity.EnemyHp);
        }

        public void OnNext(BattleState state)
        {
            switch (state)
            {
                case BattleState.Intro1:
                    _messageEntity.ToEvent("Events/Battle/BattleIntro1").Forget();
                    break;
                case BattleState.Intro2:
                    _messageEntity.ToEvent("Events/Battle/BattleIntro2").Forget();
                    break;
                case BattleState.BattleStart:
                    // ここだけView側の表示更新をawaitしてから呼ぶ
                    _battleEntity.OnNext();
                    break;
                case BattleState.MyTurn:
                case BattleState.EnemyTurn:
                    var damage = _battleEntity.CalcDamage();
                    _messageEntity.Battle(_battleEntity.Value, damage).Forget();
                    break;
                case BattleState.Win:
                    _screenEntity.OnNext(Screens.Result);
                    break;
                case BattleState.Lose:
                    _screenEntity.OnNext(Screens.Lose);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}
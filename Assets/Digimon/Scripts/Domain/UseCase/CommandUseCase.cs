using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Domain.Entity;

namespace Digimon.Digimon.Scripts.Domain.UseCase
{
    public sealed class CommandUseCase
    {
        private readonly ScreenEntity _screenEntity;
        private readonly StatusEntity _statusEntity;
        private readonly MonsterTypeEntity _monsterTypeEntity;
        private readonly DateTimeEntity _dateTimeEntity;
        private readonly MonsterAnimationEntity _monsterAnimationEntity;
        private readonly StaminaEntity _staminaEntity;

        public CommandUseCase(ScreenEntity screenEntity, StatusEntity statusEntity, MonsterTypeEntity monsterTypeEntity,
            DateTimeEntity dateTimeEntity, MonsterAnimationEntity monsterAnimationEntity, StaminaEntity staminaEntity)
        {
            _screenEntity = screenEntity;
            _statusEntity = statusEntity;
            _monsterTypeEntity = monsterTypeEntity;
            _dateTimeEntity = dateTimeEntity;
            _monsterAnimationEntity = monsterAnimationEntity;
            _staminaEntity = staminaEntity;
        }

        // csvからロードしたメッセージを表示して、次へをタップした時に2カラム目以降に記載されたコマンドを実行
        public void InvokeCommand(string[] commands)
        {
            var command = commands[0];
            if (string.IsNullOrEmpty(command)) return;
            var parameter = commands[1];
            switch (command)
            {
                case "plus_atk":
                    _statusEntity.Plus(atk: int.Parse(parameter));
                    break;
                case "plus_hp":
                    _statusEntity.Plus(hp: int.Parse(parameter));
                    break;
                case "plus_def":
                    _statusEntity.Plus(def: int.Parse(parameter));
                    break;
                case "plus_speed":
                    _statusEntity.Plus(speed: int.Parse(parameter));
                    break;
                case "plus_skill":
                    _statusEntity.Plus(skillLevel: int.Parse(parameter));
                    break;
                case "evolution":
                    _monsterTypeEntity.Evolution(_statusEntity.Hp, _statusEntity.Atk, _statusEntity.Def,
                        _statusEntity.Speed);
                    break;
                case "lose":
                    _statusEntity.Lose();
                    _monsterTypeEntity.Lose();
                    _dateTimeEntity.Lose();
                    break;
                case "animation":
                    _monsterAnimationEntity.InvokeCommand(parameter);
                    break;
                case "battle":
                    // entityにダメージを流して次のターンのobservableを流してもらう
                    break;
                // トレーニングの最後の行のコマンド 確率で共通イベントを起こす
                case "try_event":
                    if (_screenEntity.TryCommonEvent()) break;
                    _dateTimeEntity.Next();
                    break;
                // イベントの最後の行のコマンド
                case "to_menu":
                    _screenEntity.OnNext(Screens.Menu);
                    break;
                case "play_se":
                    break;
                case "play_bgm":
                    break;
                // イベントの最後の行のコマンド
                case "next":
                    _dateTimeEntity.Next();
                    break;
                case "stamina":
                    _staminaEntity.UseOrRecover(int.Parse(parameter));
                    break;
            }
        }
    }
}
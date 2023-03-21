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

        public CommandUseCase(ScreenEntity screenEntity, StatusEntity statusEntity, MonsterTypeEntity monsterTypeEntity,
            DateTimeEntity dateTimeEntity)
        {
            _screenEntity = screenEntity;
            _statusEntity = statusEntity;
            _monsterTypeEntity = monsterTypeEntity;
            _dateTimeEntity = dateTimeEntity;
        }

        // csvからロードしたメッセージを表示して、次へをタップした時に2カラム目以降に記載されたコマンドを実行
        public void InvokeCommand(string[] commands)
        {
            var index = 0;
            while (commands.Length > index)
            {
                var command = commands[index];
                if (string.IsNullOrEmpty(command)) return;
                var parameter = commands[index + 1];
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
                    case "reset":
                        _statusEntity.Lose();
                        _monsterTypeEntity.Lose();
                        break;
                    case "animation":
                        break;
                    case "battle":
                        // entityにダメージを流して次のターンのobservableを流してもらう
                        break;
                    case "to_menu":
                        _screenEntity.OnNext(Screens.Menu);
                        break;
                    case "play_se":
                        break;
                    case "next":
                        _dateTimeEntity.Next();
                        break;
                }

                index += 2;
            }
        }
    }
}
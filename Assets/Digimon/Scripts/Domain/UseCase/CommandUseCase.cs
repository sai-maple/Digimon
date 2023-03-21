using System.Linq;
using Digimon.Digimon.Scripts.Domain.Entity;

namespace Digimon.Digimon.Scripts.Domain.UseCase
{
    public sealed class CommandUseCase
    {
        private readonly StatusEntity _statusEntity;
        private readonly MonsterTypeEntity _monsterTypeEntity;

        // csvからロードしたメッセージを表示して、次へをタップした時に2カラム目以降に記載されたコマンドを実行
        public void LoadCommand(string[] csv)
        {
            var command = csv[0];
            if (string.IsNullOrEmpty(command)) return;

            switch (command)
            {
                case "plus_status":
                    var status = csv.Skip(1).Select(int.Parse).ToArray();
                    _statusEntity.Plus(status);
                    break;
                case "evolution":
                    _monsterTypeEntity.Evolution(_statusEntity.Hp, _statusEntity.Atk, _statusEntity.Def, _statusEntity.Speed);
                    break;
            }
        }
    }
}
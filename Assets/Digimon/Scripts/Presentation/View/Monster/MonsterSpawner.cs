using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Applications.Enums;
using UnityEngine;

namespace Digimon.Digimon.Scripts.Presentation.View.Monster
{
    public sealed class MonsterSpawner : MonoBehaviour
    {
        private GameObject _monster;

        public async UniTask SpawnAsync(MonsterName monsterName)
        {
            if (_monster != null) Destroy(_monster);
            _monster = null;
            var fileName = $"Monsters/{monsterName}";
            var prefab = await Resources.LoadAsync(fileName) as GameObject;
            _monster = Instantiate(prefab, transform);
        }
    }
}
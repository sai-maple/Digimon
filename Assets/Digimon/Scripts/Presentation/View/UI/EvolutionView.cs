using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Extension;
using Digimon.Digimon.Scripts.Presentation.View.Monster;
using UnityEngine;
using UnityEngine.Playables;

namespace Digimon.Digimon.Scripts.Presentation.View.UI
{
    public sealed class EvolutionView : MonoBehaviour
    {
        [SerializeField] private MonsterSpawner _spawner;
        [SerializeField] private PlayableDirector _evolutionPresent;
        [SerializeField] private PlayableDirector _evolutionDismiss;

        public async UniTask SpawnAsync(MonsterName monsterName)
        {
            await _spawner.SpawnAsync(monsterName);
        }

        public async UniTask EvolutionAsync(MonsterName evoluteName)
        {
            await _evolutionPresent.PlayAsync();
            await _spawner.SpawnAsync(evoluteName);
            await _evolutionDismiss.PlayAsync();
        }
    }
}
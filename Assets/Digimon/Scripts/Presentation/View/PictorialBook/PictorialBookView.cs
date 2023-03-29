using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Applications.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Digimon.Digimon.Scripts.Presentation.View.PictorialBook
{
    public sealed class PictorialBookView : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private TextMeshProUGUI _text;

        public PictorialBookView Create(Transform content)
        {
            return Instantiate(this, content);
        }
        
        public async void Initialize(MonsterName monsterName, bool isReleased)
        {
            _text.text = EvolutionTypeExtension.GetType(monsterName).GetLabel();
            var fileName = $"Monsters/{monsterName}";
            var prefab = await Resources.LoadAsync(fileName) as GameObject;
            var monster = Instantiate(prefab, _content);
            if (isReleased) return;
            var monsterTextures = monster.GetComponentsInChildren<Image>();
            foreach (var texture in monsterTextures)
            {
                texture.color = new Color(0.08f, 0.08f, 0.08f);
            }
        }
    }
}
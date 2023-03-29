using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Applications.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Digimon.Digimon.Scripts.Presentation.View
{
    public sealed class PictureBookView : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private TextMeshProUGUI _text;

        public PictureBookView Create(Transform content)
        {
            return Instantiate(this, content);
        }
        
        public async void Initialize(MonsterName monsterName, bool isReleased)
        {
            _text.text = EvolutionTypeExtension.GetType(monsterName).GetLabel();
            var fileName = $"Monsters/{monsterName}";
            var prefab = await Resources.LoadAsync(fileName) as GameObject;
            if (isReleased) return;
            var monsterTextures = Instantiate(prefab, _content).GetComponentsInChildren<Image>();
            foreach (var texture in monsterTextures)
            {
                texture.color = new Color(0.08f, 0.08f, 0.08f);
            }
        }
    }
}
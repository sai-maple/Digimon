using Digimon.Digimon.Scripts.Applications.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Digimon.Digimon.Scripts.Presentation.View.Audio
{
    [RequireComponent(typeof(Button))]
    public sealed class ButtonAudio : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(() => AudioView.Instance.PlaySe(Se.Tap));
        }

        private void Reset()
        {
            _button = GetComponent<Button>();
        }
    }
}
using TMPro;
using UnityEngine;

namespace Digimon.Digimon.Scripts.Presentation.View.UI
{
    public sealed class CrashRateView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _rate;

        public void Display(int rate)
        {
            var rateText = rate == 0 ? $"{rate}%" : $"<color=#FF7742>{rate}%</color>";
            _rate.text = $"けが率 {rateText}";
        }
    }
}
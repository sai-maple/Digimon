using Digimon.Digimon.Scripts.Applications.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Digimon.Digimon.Scripts.Presentation.View.UI
{
    public sealed class CommonView : MonoBehaviour
    {
        [SerializeField] private TextMeshPro dateText;
        [SerializeField] private TextMeshPro timeText;
        [SerializeField] private TextMeshPro staminaText;
        [SerializeField] private StatusView hpText;
        [SerializeField] private StatusView atkText;
        [SerializeField] private StatusView defText;
        [SerializeField] private StatusView speedText;
        [SerializeField] private StatusView skillLevelText;
        [SerializeField] private Slider staminaSlider;

        public void OnDateChanged(int date)
        {
            dateText.text = $"{date}<size=18>日目</size>";
        }
        
        public void OnTimeChanged(GameTime game)
        {
            timeText.text = game.Label();
            timeText.color = game.Color();
        }

        public void OnStaminaChanged(int value)
        {
            staminaText.text = $"{value} / 100";
            staminaSlider.value = value / 100f;
        }

        public void OnHpChanged(int previous, int current)
        {
            hpText.Display(previous, current);
        }
        
        public void OnAtkChanged(int previous, int current)
        {
            atkText.Display(previous, current);
        }
        
        public void OnDefChanged(int previous, int current)
        {
            defText.Display(previous, current);
        }
        
        public void OnSpeedChanged(int previous, int current)
        {
            speedText.Display(previous, current);
        }
        
        public void OnSkillLevelChanged(int previous, int current)
        {
            skillLevelText.Display(previous, current);
        }
    }
}
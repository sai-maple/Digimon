using Digimon.Digimon.Scripts.Applications.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Digimon.Digimon.Scripts.Presentation.View.UI
{
    public sealed class CommonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dateText;
        [SerializeField] private TextMeshProUGUI atoText;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI staminaText;
        [SerializeField] private StatusView hpText;
        [SerializeField] private StatusView atkText;
        [SerializeField] private StatusView defText;
        [SerializeField] private StatusView speedText;
        [SerializeField] private StatusView skillLevelText;
        [SerializeField] private Slider staminaSlider;

        public void Initialize(int hp, int atk, int def, int speed, int skillLevel)
        {
            hpText.Display(hp, hp);
            atkText.Display(atk, atk);
            defText.Display(def, def);
            speedText.Display(speed, speed);
            skillLevelText.Display(skillLevel, skillLevel);
        }

        public void OnDateChanged(int date)
        {
            dateText.text = $"{date}<size=32>日目</size>";
            var ato = date % 10;
            atoText.text = $"<size=32>試験まで あと</size><color=#FF0063>{10 - ato}<size=32>日</size>";
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
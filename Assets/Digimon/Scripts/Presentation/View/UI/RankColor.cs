using Digimon.Digimon.Scripts.Applications.Enums;
using UnityEngine;

namespace Digimon.Digimon.Scripts.Presentation.View.UI
{
    [CreateAssetMenu(menuName = "Digimon/RankColor")]
    public sealed class RankColor : ScriptableObject
    {
        [SerializeField] private Color[] colors;

        public Color Get(StatusRank rank)
        {
            return colors[(int)rank];
        }
    }
}
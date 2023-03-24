using Digimon.Digimon.Scripts.Applications.Enums;
using UnityEngine;

namespace Digimon.Digimon.Scripts.Presentation.View.Monster
{
    public sealed class MonsterAnimationView : MonoBehaviour
    {
        [SerializeField] private Animator _reaction;

        public void SetTrigger(MonsterReaction reaction)
        {
            _reaction.SetTrigger(reaction.ToString());
        }
    }
}
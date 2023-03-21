using Digimon.Digimon.Scripts.Applications.Enums;
using UnityEngine;

namespace Digimon.Digimon.Scripts.Presentation.View.Monster
{
    public sealed class MonsterAnimationView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void SetTrigger(MonsterReaction reaction)
        {
            _animator.SetTrigger(reaction.ToString());
        }
    }
}
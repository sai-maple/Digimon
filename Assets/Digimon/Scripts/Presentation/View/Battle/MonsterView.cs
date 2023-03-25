using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Digimon.Digimon.Scripts.Presentation.View.Battle
{
    public sealed class MonsterView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Slider _gauge;
        [SerializeField] private TextMeshProUGUI _hpText;
        [SerializeField] private TextMeshProUGUI _damageText;
        [SerializeField] private CanvasGroup _uiCanvas;

        private DOTweenTMPAnimator _tmpAnimator;
        private int _maxHp;

        private void Awake()
        {
            _tmpAnimator = new DOTweenTMPAnimator(_damageText);
        }

        public void Initialize(int hp, int y = 0)
        {
            _gauge.value = 1;
            _hpText.text = $"{hp} : {hp}";
            _maxHp = hp;
            _animator.SetBool(Battle.Down, false);
            _damageText.DOFade(0, 0);
            transform.DOLocalMoveY(y, 0);
            _uiCanvas.alpha = 0;
        }

        public void DoFadeUi(float endValue)
        {
            _uiCanvas.DOFade(endValue, 0.5f);
        }

        public void Attack()
        {
            _animator.SetTrigger(Battle.Attack);
        }

        public async UniTask PresentAsync()
        {
            await transform.DOLocalMoveY(0, 0.5f);
        }

        public void TakeDamage(int damage, int hp)
        {
            _damageText.text = $"-{damage}";
            _hpText.text = $"{hp} : {_maxHp}";
            for (var i = 0; i < _tmpAnimator.textInfo.characterCount; i++)
            {
                DOTween.Sequence()
                    .Append(_tmpAnimator.DOOffsetChar(i, Vector3.up * 20, 0.25f))
                    .Join(_tmpAnimator.DOFadeChar(i, 1, 0.1f))
                    .Append(_tmpAnimator.DOOffsetChar(i, Vector3.zero, 0.25f))
                    .SetDelay(0.2f * i);
            }

            _animator.SetTrigger(Battle.Damage);
            _gauge.DOValue((float)hp / _maxHp, 0.5f).SetEase(Ease.Linear);
            _damageText.DOFade(0, 0.5f).SetDelay(2);
            _animator.SetBool(Battle.Down, hp <= 0);
        }

        private class Battle
        {
            public static readonly int Attack = Animator.StringToHash("Attack");
            public static readonly int Damage = Animator.StringToHash("Damage");
            public static readonly int Down = Animator.StringToHash("Down");
        }
    }
}
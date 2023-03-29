using Cysharp.Threading.Tasks;
using DG.Tweening;
using Digimon.Digimon.Scripts.Extension;
using UnityEngine;
using UnityEngine.Playables;

namespace Digimon.Digimon.Scripts.Presentation.View.UI
{
    public sealed class FadeView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _content;
        [SerializeField] private PlayableDirector _present;
        [SerializeField] private PlayableDirector _dismiss;
        
        public async UniTask FadePresent()
        {
            await _present.PlayAsync();
        }
        
        public async UniTask FadeDismiss()
        {
            await _dismiss.PlayAsync();
        }

        public async UniTask ContentCadeAsync(float endValue)
        {
            await _content.DOFade(endValue, 0.5f);
        }
    }
}
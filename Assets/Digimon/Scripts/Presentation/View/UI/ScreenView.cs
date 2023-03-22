using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Digimon.Digimon.Scripts.Presentation.View.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ScreenView : MonoBehaviour
    {
        [SerializeField] private Vector3 from;
        [SerializeField] private Vector3 to;
        [SerializeField] private CanvasGroup canvasGroup;

        protected bool IsShow;

        private void Awake()
        {
            transform.localPosition = from;
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            IsShow = false;
        }

        public virtual async UniTask PresentAsync()
        {
            if (IsShow) return;
            IsShow = true;
            var token = this.GetCancellationTokenOnDestroy();
            var tween = transform.DOLocalMove(to, 0.5f).WithCancellation(token);
            var fade = canvasGroup.DOFade(1, 0.5f).WithCancellation(token);
            await UniTask.WhenAll(tween, fade);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public virtual async UniTaskVoid DismissAsync()
        {
            if (!IsShow) return;
            IsShow = false;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            var token = this.GetCancellationTokenOnDestroy();
            var tween = transform.DOLocalMove(from, 0.5f).WithCancellation(token);
            var fade = canvasGroup.DOFade(0, 0.5f).WithCancellation(token);
            await UniTask.WhenAll(tween, fade);
        }

        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}
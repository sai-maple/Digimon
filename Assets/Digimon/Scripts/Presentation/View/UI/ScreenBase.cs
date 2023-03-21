using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Digimon.Digimon.Scripts.Presentation.View.UI
{
    public abstract class ScreenBase : MonoBehaviour
    {
        [SerializeField] private Vector3 from;
        [SerializeField] private Vector3 to;
        [SerializeField] private CanvasGroup canvasGroup;

        private bool _isShow;

        private void Awake()
        {
            transform.localPosition = from;
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            _isShow = false;
        }

        public async UniTaskVoid PresentAsync()
        {
            if (_isShow) return;
            _isShow = true;
            var token = this.GetCancellationTokenOnDestroy();
            var tween = transform.DOLocalMove(to, 0.5f).WithCancellation(token);
            var fade = canvasGroup.DOFade(1, 0.5f).WithCancellation(token);
            await UniTask.WhenAll(tween, fade);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public async UniTaskVoid DismissAsync()
        {
            if (!_isShow) return;
            _isShow = false;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            var token = this.GetCancellationTokenOnDestroy();
            var tween = transform.DOLocalMove(from, 0.5f).WithCancellation(token);
            var fade = canvasGroup.DOFade(0, 0.5f).WithCancellation(token);
            await UniTask.WhenAll(tween, fade);
        }
    }
}
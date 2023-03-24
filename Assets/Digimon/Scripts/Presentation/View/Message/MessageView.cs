using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Digimon.Digimon.Scripts.Presentation.View.Message
{
    public sealed class MessageView : MonoBehaviour
    {
        [SerializeField] private RectTransform _messageBox;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _message;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Vector3 _from;
        [SerializeField] private Vector3 _to;

        public void Initialize()
        {
            _messageBox.anchoredPosition = _from;
            _message.text = "";
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public async UniTask PresentAsync(CancellationToken token)
        {
            // todo シュって音出したい?
            _message.text = "";
            await _messageBox.DOAnchorPos(_to, 0.2f).WithCancellation(token);
            if (token.IsCancellationRequested) return;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public async UniTaskVoid DismissAsync(CancellationToken token)
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            await _messageBox.DOAnchorPos(_from, 0.2f).WithCancellation(token);
        }

        public async UniTask MessageAsync(string message, CancellationToken token)
        {
            if (string.IsNullOrEmpty(message)) return;
            var cancellation = new CancellationTokenSource();
            _message.text = "";
            _audioSource.Play();
            var buttonTask = _nextButton.GetAsyncClickEventHandler();
            var textTween = _message.DOText(message, message.Length * 0.1f);

            // ボタンクリックでメッセージ表示スキップ
            var index = await UniTask.WhenAny(buttonTask.OnClickAsync(), textTween.WithCancellation(cancellation.Token));
            // 呼び出し元が死んでたらreturn
            if (token.IsCancellationRequested) return;
            if (index == 0)
            {
                cancellation.Cancel();
                cancellation.Dispose();
                cancellation = null;
                textTween.Kill();
            }

            _message.text = message;
            // カタカタ音停止 + もう一度タップで
            _audioSource.Stop();
            await buttonTask.OnClickAsync();
            cancellation?.Cancel();
            cancellation?.Dispose();
        }
    }
}
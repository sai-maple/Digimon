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
        [SerializeField] private RectTransform messageBox;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI message;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Button nextButton;
        [SerializeField] private Vector3 from;
        [SerializeField] private Vector3 to;

        public void Initialize()
        {
            messageBox.anchoredPosition = from;
            message.text = "";
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public async UniTask PresentAsync(CancellationToken token)
        {
            // todo シュって音出したい?
            message.text = "";
            await messageBox.DOAnchorPos(to, 0.5f).WithCancellation(token);
            if (token.IsCancellationRequested) return;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public async UniTaskVoid DismissAsync(CancellationToken token)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            await messageBox.DOAnchorPos(from, 0.5f).WithCancellation(token);
        }

        public async UniTask MessageAsync(string message, CancellationToken token)
        {
            if (string.IsNullOrEmpty(message)) return;
            var cancellation = new CancellationTokenSource();
            audioSource.Play();
            var buttonTask = nextButton.GetAsyncClickEventHandler();
            var textTask = this.message.DOText(message, message.Length * 0.1f).WithCancellation(cancellation.Token);

            // ボタンクリックでメッセージ表示スキップ
            var index = await UniTask.WhenAny(buttonTask.OnClickAsync(), textTask);
            // 呼び出し元が死んでたらreturn
            if (token.IsCancellationRequested) return;
            if (index == 0)
            {
                cancellation.Cancel();
                cancellation.Dispose();
                cancellation = null;
                this.message.text = message;
            }

            // カタカタ音停止 + もう一度タップで
            audioSource.Stop();
            await buttonTask.OnClickAsync();
            cancellation?.Cancel();
            cancellation?.Dispose();
        }
    }
}
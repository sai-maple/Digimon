using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Digimon.Digimon.Scripts.Applications.Static;
using Digimon.Digimon.Scripts.Presentation.View.Message;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.Intro
{
    public sealed class IntroPresenter : MonoBehaviour
    {
        [SerializeField] private MessageView _messageView;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private ConfirmView _confirmView;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private CanvasGroup glow;

        private async void Awake()
        {
            _inputField.onEndEdit.AddListener(text => _text.text = $"「{text}」でいい？");
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            var token = this.GetCancellationTokenOnDestroy();
            _messageView.Initialize();

            // 名前を決めるまで
            var textAsset = await ToEvent("Events/Tutorial1");
            await _messageView.PresentAsync(token);
            if (token.IsCancellationRequested) return;
            if (textAsset == null) return;
            await MessageAsync(new StringReader(textAsset.text), token);
            if (token.IsCancellationRequested) return;
            await _messageView.DismissAsync(token);
            if (token.IsCancellationRequested) return;

            // 入力表示
            await _canvasGroup.DOFade(1, 0.5f);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _confirmButton.onClick.AddListener(() => _confirmView.gameObject.SetActive(true));
            var asyncEvent = _confirmView.ConfirmButton.onClick.GetAsyncEventHandler(token);
            await asyncEvent.OnInvokeAsync();
            _confirmView.gameObject.SetActive(false);

            MonsterName.Name = _inputField.text;
            glow.DOFade(0, 1);
            // よろしくね
            textAsset = await ToEvent("Events/Tutorial2");
            await _messageView.PresentAsync(token);
            if (token.IsCancellationRequested) return;
            await MessageAsync(new StringReader(textAsset.text), token);
            if (token.IsCancellationRequested) return;
            await _messageView.DismissAsync(token);
            SceneManager.LoadScene($"Main");
        }

        private async UniTask MessageAsync(TextReader reader, CancellationToken token)
        {
            await _messageView.PresentAsync(token);
            while (reader.Peek() != -1)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(line)) continue;
                var messageAndCommand = line.Split(',');
                await _messageView.MessageAsync(messageAndCommand[0], token);
            }

            _messageView.DismissAsync(token).Forget();
        }

        private async UniTask<TextAsset> ToEvent(string fileName)
        {
            return await Resources.LoadAsync(fileName) as TextAsset;
        }
    }
}
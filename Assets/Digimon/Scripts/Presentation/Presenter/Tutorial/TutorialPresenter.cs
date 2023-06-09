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

namespace Digimon.Digimon.Scripts.Presentation.Presenter.Tutorial
{
    public sealed class TutorialPresenter : MonoBehaviour
    {
        [SerializeField] private MessageView _messageView;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private ConfirmView _confirmView;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private CanvasGroup glow;

        [SerializeField] private CanvasGroup _mask;

        private async void Awake()
        {
            _mask.DOFade(0, 2).ToUniTask().Forget();
            _confirmView.gameObject.SetActive(false);
            _confirmButton.onClick.AddListener(() => _confirmView.gameObject.SetActive(true));
            _text.text = $"「{StaticMonsterName.Name}」でいい？";
            _inputField.text = StaticMonsterName.Name;
            _inputField.onEndEdit.AddListener(text => _text.text = $"「{text}」でいい？");
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            var token = this.GetCancellationTokenOnDestroy();
            _messageView.Initialize();

            // 名前を決めるまで
            var textAsset = await ToEvent("Events/Events/Tutorial/Tutorial1");
            await MessageAsync(new StringReader(textAsset.text), token);
            if (token.IsCancellationRequested) return;

            // 入力表示
            await _canvasGroup.DOFade(1, 0.5f);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            var asyncEvent = _confirmView.ConfirmButton.onClick.GetAsyncEventHandler(token);
            await asyncEvent.OnInvokeAsync();
            _confirmView.gameObject.SetActive(false);
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            StaticMonsterName.Name = _inputField.text;
            glow.DOFade(0, 1).ToUniTask(cancellationToken: token).Forget();
            // よろしくね
            textAsset = await ToEvent("Events/Events/Tutorial/Tutorial2");
            await MessageAsync(new StringReader(textAsset.text), token);
            if (token.IsCancellationRequested) return;
            await _mask.DOFade(1, 1);
            SceneManager.LoadScene($"MainScene");
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
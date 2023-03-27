using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Digimon.Digimon.Scripts.Presentation.View.UI
{
    public sealed class WinScreenView : MonoBehaviour
    {
        [SerializeField] private Button _rankingButton;
        [SerializeField] private Button _tweetButton;
        [SerializeField] private Button _returnButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Vector3 _from;
        private int _date;

        public IObservable<Unit> OnRankingTapAsObservable()
        {
            return _rankingButton.OnClickAsObservable();
        }

        public IObservable<Unit> OnReturnTapAsObservable()
        {
            return _returnButton.OnClickAsObservable();
        }

        private void Awake()
        {
            _tweetButton.onClick.AddListener(() =>
            {
                StartCoroutine(
                    TweetWithScreenShot.TweetManager.TweetWithScreenShot($"きみと出会った日を{_date}日でクリアしたよ！"));
            });
        }

        public void Initialize()
        {
            transform.localPosition = _from;
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public async UniTask Present(int date)
        {
            _date = date;
            _canvasGroup.DOFade(1, 0.5f).ToUniTask().Forget();
            await transform.DOLocalMove(Vector3.zero, 0.5f);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}
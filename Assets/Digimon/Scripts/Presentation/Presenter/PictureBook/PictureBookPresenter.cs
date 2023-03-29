using System;
using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Applications.Static;
using Digimon.Digimon.Scripts.Presentation.View;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer.Unity;
using MonsterName = Digimon.Digimon.Scripts.Applications.Enums.MonsterName;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.PictureBook
{
    public sealed class PictureBookPresenter : IInitializable
    {
        private readonly PictorialBookEntity _pictorialBookEntity;
        private readonly FadeView _fadeView;
        private readonly Transform _content;
        private readonly Button _returnButton;

        private readonly PictureBookView _prefab;

        public PictureBookPresenter(PictorialBookEntity pictorialBookEntity, FadeView fadeView, Transform content,
            Button returnButton)
        {
            _pictorialBookEntity = pictorialBookEntity;
            _fadeView = fadeView;
            _content = content;
            _returnButton = returnButton;
        }

        public async void Initialize()
        {
            _returnButton.onClick.AddListener(() => DismissAsync().Forget());
            _content.gameObject.SetActive(false);
            foreach (MonsterName monster in Enum.GetValues(typeof(MonsterName)))
            {
                var pictureBook = _prefab.Create(_content);
                pictureBook.Initialize(monster, _pictorialBookEntity.Book[monster]);
            }

            await _fadeView.FadePresent();
            await _fadeView.ContentCadeAsync(1);
        }

        private async UniTaskVoid DismissAsync()
        {
            _fadeView.ContentCadeAsync(1).Forget();
            await _fadeView.FadeDismiss();
            await SceneManager.UnloadSceneAsync("PictureBook");
        }
    }
}
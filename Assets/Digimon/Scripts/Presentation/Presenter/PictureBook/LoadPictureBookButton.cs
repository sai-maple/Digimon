using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.PictureBook
{
    public sealed class LoadPictureBookButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(() =>
            {
                if (Enumerable.Range(0, SceneManager.sceneCount).Select(i => SceneManager.GetSceneAt(i).name)
                    .Any(n => n == "PictureBook")) return;
                SceneManager.LoadScene($"PictureBook", LoadSceneMode.Additive);
            });
        }
    }
}
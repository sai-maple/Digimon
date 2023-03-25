using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Digimon.Digimon.Scripts.Presentation.View.UI
{
    public sealed class ConfirmView : MonoBehaviour
    {
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _returnButton;

        private void Awake()
        {
            gameObject.SetActive(false);
            _confirmButton.onClick.AddListener(() => SceneManager.LoadScene($"Tutorial"));
            _returnButton.onClick.AddListener(() => gameObject.SetActive(false));
        }

        public void Present()
        {
            gameObject.SetActive(true);
        }
    }
}
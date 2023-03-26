using System;
using UnityEngine;
using UnityEngine.UI;

namespace Digimon.Digimon.Scripts.Presentation.View.UI
{
    public sealed class ConfirmView : MonoBehaviour
    {
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _returnButton;

        public Button ConfirmButton => _confirmButton;

        private Action _callBack;

        private void Awake()
        {
            gameObject.SetActive(false);
            _confirmButton.onClick.AddListener(() => _callBack?.Invoke());
            _returnButton.onClick.AddListener(() => gameObject.SetActive(false));
        }

        public void Present(Action callBack)
        {
            _callBack = callBack;
            gameObject.SetActive(true);
        }
    }
}
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIWindow : MonoBehaviour
    {
        [SerializeField] private bool _isOpenOnStart = false;
        [SerializeField] private float _delay = 0.25f;
        [SerializeField] private Ease _ease = Ease.Linear;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private UnityEvent _openEvent;
        [SerializeField] private UnityEvent _closeEvent;
        private bool _isOpen = false;
        private void Start() {
            if (_isOpenOnStart) Open(true);
        }
        public void Open(bool isOpen)
        {
            if (_isOpen == isOpen) return;
            _isOpen = isOpen;
            if (_isOpen) {
                gameObject.SetActive(true);
            }
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = _isOpen ? 0 : 1;
            _canvasGroup.DOFade(_isOpen ? 1 : 0, _delay).SetEase(_ease).OnComplete(AfterEvent);
        }
        private void AfterEvent()
        {
            gameObject.SetActive(_isOpen);
            if (_isOpen) {
                _canvasGroup.interactable = true;
                _openEvent.Invoke();
            } else {
                _canvasGroup.blocksRaycasts = false;
                _closeEvent.Invoke();
            }

        }
        private void OnValidate()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null) {
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.UI
{
    public sealed class UIWindow : MonoBehaviour
    {
        private const uint UPDATE_STEP = 20;  
        [SerializeField] private bool _isOpenOnStart = false;
        [SerializeField] private float _delay = 0.25f;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private UnityEvent _openEvent;
        [SerializeField] private UnityEvent _closeEvent;
        private bool _isOpen = false;
        private void Start() {
            if (_isOpenOnStart) Open();
        }
        public void Open()
        {
            if (_isOpen) return;
            _isOpen = true;
            gameObject.SetActive(true);
            _canvasGroup.blocksRaycasts = true;
            //Debug.Log("Open");
            StartCoroutine(WindowAnimation());
        }
        public void Close()
        {
            if (!_isOpen) return;
            _isOpen = false;
            _canvasGroup.interactable = false;
            StartCoroutine(WindowAnimation());
        }
        private IEnumerator WindowAnimation()
        {
            var currentTime = 0f;
            var updateTick = _delay / UPDATE_STEP;
            while (currentTime < _delay) {
                var alpha = currentTime / _delay;
                currentTime += updateTick;
                _canvasGroup.alpha = _isOpen ? alpha : 1 - alpha;
                yield return new WaitForSecondsRealtime(updateTick);
            }
            _canvasGroup.alpha = _isOpen ? 1 : 0;
            gameObject.SetActive(_isOpen);
            if (_isOpen) {
                _canvasGroup.interactable = true;
                _openEvent.Invoke();
            }
            else {
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

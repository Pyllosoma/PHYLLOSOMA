using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.UI.Components.Animations;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIWindow : MonoBehaviour
    {
        [SerializeField] private float _delay = 0.25f;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private List<UIAnimation> _windowAnimations;
        [SerializeField] private UnityEvent _openEvent;
        [SerializeField] private UnityEvent _closeEvent;
        private bool _isOpen = false;
        private void Awake() {
            _isOpen = gameObject.activeSelf;
        }
        public void Open(){
            Open(true);
        }
        public void Close(){
            Open(false);
        }
        public void Open(bool isOpen)
        {
            if (_isOpen == isOpen) return;
            _isOpen = isOpen;
            if (_isOpen) {
                gameObject.SetActive(true);
                _openEvent.Invoke();
            }
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = false;
            foreach (var uiAnimation in _windowAnimations) {
                if (_isOpen) {
                    uiAnimation.Play();
                } else {
                    uiAnimation.Rewind();
                }
            }
            Invoke(nameof(AfterEvent), _delay);
        }
        private void AfterEvent()
        {
            gameObject.SetActive(_isOpen);
            if (_isOpen) {
                _canvasGroup.interactable = true;
            } else {
                _canvasGroup.blocksRaycasts = false;
                _closeEvent.Invoke();
            }

        }
        private void OnValidate()
        {
            float max = 0;
            foreach (var windowAnimation in _windowAnimations) {
                if (!windowAnimation) {
                    continue;
                }
                if (windowAnimation.AnimationTime > max) {
                    max = windowAnimation.AnimationTime;
                }
            }
            _delay = max;
        }
    }
}

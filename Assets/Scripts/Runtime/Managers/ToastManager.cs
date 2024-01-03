using System;
using System.Collections.Generic;
using Runtime.Patterns;
using Runtime.UI;
using Runtime.UI.Windows;
using UnityEngine;

namespace Runtime.Managers
{
    //알림을 위한 토스트 시스템
    public class ToastManager : Singleton<ToastManager>
    {
        public const float TOAST_DURATION_LONG = 2f;
        public const float TOAST_DURATION_SHORT = 1f;
        struct ToastRequest {
            public float Duration;
            public string Message;
        }
        
        [SerializeField] private ToastWindow _toastWindow;
        private readonly Queue<ToastRequest> _toastRequests = new Queue<ToastRequest>();
        private float _closeTimer = 0f;
        private float _currentToastDuration = 0f;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) {
                Show("Hellow World " + _toastRequests.Count); 
                Debug.Log(_toastRequests.Count);
            }
            CheckToast();
            if (!_toastWindow.gameObject.activeSelf) return;
            _closeTimer += Time.unscaledDeltaTime;
            if (_closeTimer < _currentToastDuration) return;
            _toastWindow.Open(false);
        }
        public void CheckToast() {
            if (_toastRequests.Count == 0) return;
            if (_toastWindow.gameObject.activeSelf) return;
            var request = _toastRequests.Dequeue();
            _toastWindow.Init(request.Message);
            _currentToastDuration = request.Duration;
            _closeTimer = 0f;
        }

        private void ShowToast(string message, float duration = TOAST_DURATION_SHORT)
        {
            _toastRequests.Enqueue(new ToastRequest() {
                Duration = duration,
                Message = message
            });
            //Debug.Log(_toastRequests.Count);
            CheckToast();
        }
        public static void Show(string message, float duration = TOAST_DURATION_SHORT) {
            if (!Instance) return;
            Instance.ShowToast(message, duration);
        }
    }
}
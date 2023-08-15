using System;
using System.Collections;
using DG.Tweening;
using Runtime.UI.Components.Animations;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Components.Info.Logs
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ValueLog : MonoBehaviour
    {
        [SerializeField] private float _logLifeTime = 1f;
        [SerializeField] UIAnimation _fadeAnimation;
        [SerializeField] UIAnimation _resizeAnimation;
        private TextMeshProUGUI _logText;
        private Action _onComplete = null;
        private void Awake(){
            _logText = GetComponentInChildren<TextMeshProUGUI>();
        }
        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.Space)) {
        //         InitLog("Test",true);
        //     }
        // }
        public void InitLog(string value,bool disappearAuto = false , float logLifeTime = 1f , Action onComplete = null)
        {
            _logText.text = value;
            _logLifeTime = logLifeTime;
            _onComplete = onComplete;
            _fadeAnimation.Play(() => {
                if (!disappearAuto) return;
                Invoke(nameof(DisappearLog),_logLifeTime);
            });
        }
        public void DisappearLog()
        {
            _fadeAnimation.Rewind(() => {
                _resizeAnimation.Play(() => {
                    _onComplete?.Invoke();
                    Destroy(gameObject);
                });
            });
        }
    }
}
using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Components.Info.ValueIndicators
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ValueLog : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _resizeCurve = AnimationCurve.Constant(0, 1, 1f);
        [SerializeField] private Ease _fadeEase = Ease.Linear;
        [SerializeField] private float _logShowTime = 0.25f;
        [SerializeField] private float _logLifeTime = 1f;
        [SerializeField] private float _logResizeTime = 1f;
        [SerializeField] private int _logResizePerSecond = 30;
        [SerializeField] private Vector2 _logTargetScale = new Vector2();
        
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private TextMeshProUGUI _logText;
        private Action _onComplete = null;
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _logText = GetComponentInChildren<TextMeshProUGUI>();
        }
        private void Start(){
            _logTargetScale = new Vector2(_rectTransform.sizeDelta.x,0);
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
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1f, _logShowTime).SetEase(_fadeEase).onComplete += () => {
                if (!disappearAuto) return;
                Invoke(nameof(DisappearLog),_logLifeTime);
            };
        }
        public void DisappearLog()
        {
            _canvasGroup.DOFade(0f, _logShowTime).SetEase(_fadeEase).onComplete += () => {
                StartCoroutine(MakeLogSmaller());
            };
        }
        private IEnumerator MakeLogSmaller(){
            float timePerUpdate = 1f / _logResizePerSecond;
            float timer = 0f;
            while (timer < _logResizeTime) {
                timer += timePerUpdate;
                _rectTransform.sizeDelta = Vector2.Lerp(_rectTransform.sizeDelta, _logTargetScale,_resizeCurve.Evaluate(timer / _logResizeTime));
                yield return new WaitForSeconds(timePerUpdate);
            }
            _onComplete?.Invoke();
            Destroy(gameObject);
        }
    }
}
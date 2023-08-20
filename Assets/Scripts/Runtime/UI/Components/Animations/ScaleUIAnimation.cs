using DG.Tweening;
using UnityEngine;

namespace Runtime.UI.Components.Animations
{
    public class ScaleUIAnimation : UIAnimation
    {
        [Header("Scale Animation Settings")]
        [SerializeField] private Ease _scaleEase = Ease.Linear;
        [SerializeField] private Vector3 _startScale = new Vector3(0.9f,0.9f,0.9f);
        [SerializeField] private Vector3 _endScale = new Vector3(1f,1f,1f);
        private void Awake(){
            transform.localScale = _startScale;
        }
        protected override void PlayAnimation()
        {
            transform.localScale = _startScale;
            transform.DOScale(_endScale, _animationTime).SetEase(_scaleEase).OnComplete(Complete);
        }
        protected override void RewindAnimation()
        {
            transform.localScale = _endScale;
            transform.DOScale(_startScale, _animationTime).SetEase(_scaleEase).OnComplete(Complete);
        }
    }
}
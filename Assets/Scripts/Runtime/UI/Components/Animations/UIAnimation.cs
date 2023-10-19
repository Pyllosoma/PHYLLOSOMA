using System;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.UI.Components.Animations
{
    public abstract class UIAnimation : MonoBehaviour
    {
        public float AnimationTime => _animationTime;
        [Header("Base Animation Settings")]
        [SerializeField] protected bool _useUnscaledTime = true;
        [SerializeField] protected float _animationTime = 0.25f;
        [SerializeField] protected UnityEvent _onComplete;
        private Action _onCompleteAction = null;
        public void Play(){
            Play(null);
        }
        public void Rewind(){
            Rewind(null);
        }
        public void Play(Action onComplete)
        {
            _onCompleteAction = onComplete;
            PlayAnimation();
        }
        public void Rewind(Action onComplete)
        {
            _onCompleteAction = onComplete;
            RewindAnimation();
        }
        protected abstract void PlayAnimation();
        protected abstract void RewindAnimation();
        protected void Complete()
        {
            _onCompleteAction?.Invoke();
            _onComplete.Invoke();
        }
    }
}
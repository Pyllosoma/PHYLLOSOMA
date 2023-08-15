using System;
using UnityEngine;

namespace Runtime.UI.Components.Animations
{
    public abstract class UIAnimation : MonoBehaviour
    {
        public float AnimationTime => _animationTime;
        [SerializeField] protected float _animationTime = 0.25f;
        public abstract void Play(Action onComplete = null);
        public abstract void Rewind(Action onComplete = null);
    }
}
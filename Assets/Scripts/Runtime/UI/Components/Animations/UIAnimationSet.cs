using System;
using UnityEngine;

namespace Runtime.UI.Components.Animations
{
    public class UIAnimationSet : MonoBehaviour
    {
        [SerializeField] private UIAnimation[] _animations;
        public void Play()
        {
            foreach (var an in _animations) {
                an.Play();
            }
        }
        public void Rewind()
        {
            foreach (var an in _animations) {
                an.Rewind();
            }
        }
    }
}
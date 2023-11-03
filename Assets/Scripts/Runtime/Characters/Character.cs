using System;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Characters
{
    public abstract class Character : MonoBehaviour
    {
        public Action<Character> OnCharacterAlive;
        public Action<Character> OnCharacterDeath;
        [Header("Character Setting")]
        [SerializeField] private UnityEvent _onAlive;
        [SerializeField] private UnityEvent _onDeath;
        protected virtual void OnEnable()
        {
            Reset();
            OnAlive();
            OnCharacterAlive?.Invoke(this);
            _onAlive?.Invoke();
        }

        /// <summary>
        /// 자신이 파괴될 때 호출되는 이벤트
        /// </summary>
        protected void Death()
        {
            OnDeath();
            OnCharacterDeath?.Invoke(this);
            _onDeath?.Invoke();
            gameObject.SetActive(false);
        }
        protected abstract void OnAlive();
        protected abstract void OnDeath();
        public virtual void Reset() { }
    }
}
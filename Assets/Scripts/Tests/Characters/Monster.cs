using System;
using Runtime.Characters;
using Runtime.Data.Structure;
using Runtime.Patterns.FSM;
using Runtime.Utils;
using Tests.Characters.MonsterFSM;
using Tests.Weapons;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Tests.Characters
{
    //일단 테스트를 위해 상위 부모 클래스 없이 구현
    public abstract class Monster<T> : Character where T : Character
    {
        public IState<T> State {
            get => _state;
            set {
                _state?.Exit(this as T);
                _state = value;
                _state?.Enter(this as T);
            }
        }

        // /// <summary>
        // /// Status part
        // /// </summary>
        // public float Health
        // {
        //     get => _health;
        //     protected set
        //     {
        //         _health = value;
        //         if (_health <= 0) {
        //             Death();
        //         }
        //         if (_health >= _maxHealth) {
        //             _health = _maxHealth;
        //         }
        //         _monsterUI.SetHealth(_health);
        //     }
        // }
        // public float SoulGauge
        // {
        //     get => _soulGauge;
        //     protected set
        //     {
        //         _soulGauge = value;
        //         if (_soulGauge <= 0) {
        //             Groggy();
        //         }
        //         if (_soulGauge >= _maxSoulGauge) {
        //             _soulGauge = _maxSoulGauge;
        //         }
        //         _monsterUI.SetSoulGauge(_soulGauge);
        //     }
        // }
        // [Header("Status")]
        // [SerializeField] protected float _maxHealth = 100;
        // [SerializeField] protected float _health = 100;
        // [SerializeField] protected float _maxSoulGauge = 100f;
        // [SerializeField] protected float _soulGauge = 100f;
        
        /// <summary>
        /// State part
        /// </summary>
        private IState<T> _state = null;
        protected virtual void Update(){
            State?.Update(this as T);
        }
        protected virtual void FixedUpdate(){
            State?.FixedUpdate(this as T);
        }
    }
}
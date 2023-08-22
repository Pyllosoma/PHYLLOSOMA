using System;
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
    public class Character<T> : MonoBehaviour where T : MonoBehaviour
    {
        public IState<T> State {
            get => _state;
            set {
                _state?.Exit(this as T);
                _state = value;
                _state?.Enter(this as T);
            }
        }
        public BaseStats Stats => _stats;
        [SerializeField] private BaseStats _stats = new BaseStats();
        private IState<T> _state = null;
        private void Update(){
            State?.Update(this as T);
        }
        private void FixedUpdate(){
            State?.FixedUpdate(this as T);
        }
    }
}
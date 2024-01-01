using System;
using Runtime.Patterns.FSM;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Tests.Characters.FSM
{
    [Serializable]
    public class GameObjectFSM : IState<GameObject>
    {
        [FoldoutGroup("State Events")]
        [SerializeField] private UnityEvent _onStateEnter;
        [FoldoutGroup("State Events")]
        [SerializeField] private UnityEvent _onStateExit;
        public virtual void Enter(GameObject entity) {
            _onStateEnter?.Invoke();
        }
        public virtual void Update(GameObject entity){}
        public virtual void FixedUpdate(GameObject entity){}

        public virtual void Exit(GameObject entity) {
            _onStateExit?.Invoke();
        }
    }
}
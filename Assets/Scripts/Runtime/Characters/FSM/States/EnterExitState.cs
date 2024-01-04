using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Characters.FSM.States
{
    public class EnterExitState : GameObjectFSM
    {
        [FoldoutGroup("State Events")]
        [SerializeField] private UnityEvent _onStateEnter;
        [FoldoutGroup("State Events")]
        [SerializeField] private UnityEvent _onStateExit;
        public override void Enter(GameObject entity) {
            _onStateEnter?.Invoke();
        }
        public override void Exit(GameObject entity)
        {
            _onStateExit?.Invoke();
        }
    }
}
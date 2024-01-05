using Runtime.Utils;
using Runtime.Utils.Components;
using UnityEngine;

namespace Runtime.Characters.FSM.States
{
    public class TargetBaseState : GameObjectFSM
    {
        protected TargetableComponent _targetableComponent;
        public override void Enter(GameObject entity) {
            if (!_targetableComponent) _targetableComponent = entity.GetComponentInChildren<TargetableComponent>();
        }
    }
}
using Runtime.Utils;
using UnityEngine;

namespace Runtime.Characters.FSM.States
{
    public class TargetBaseState : GameObjectFSM
    {
        //[Title("Required Components")]
        //[SerializeField] 
        protected TargetDetector _targetDetector;
        public override void Enter(GameObject entity) {
            if (!_targetDetector) _targetDetector = entity.GetComponentInChildren<TargetDetector>();
        }
    }
}
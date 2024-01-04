using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Characters.FSM.States.Trackers
{
    public class AgentChaseState : TargetBaseState
    {
        [Title("Movement Settings")]
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _acceleration = 10f;
        [SerializeField] private float _angularSpeed = 720f;
        [Title("Required Components")]
        [SerializeField] private NavMeshAgent _agent;
        public override void Enter(GameObject entity) {
            base.Enter(entity);
            _agent.speed = _speed;
            _agent.acceleration = _acceleration;
            _agent.angularSpeed = _angularSpeed;
        }
        public override void Update(GameObject entity) {
            if (!_targetableComponent.IsTargetExist) return;
            _agent.SetDestination(_targetableComponent.Target.transform.position);
        }
        public override void Exit(GameObject entity)
        {
            //Make don't move statue
            _agent.SetDestination(entity.transform.position);
            _agent.velocity = Vector3.zero;
            _agent.angularSpeed = 0f;
            _agent.speed = 0f;
            _agent.acceleration = 0f;
        }
    }
}
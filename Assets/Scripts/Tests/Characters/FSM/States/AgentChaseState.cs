using Runtime.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Tests.Characters.FSM.States
{
    public class AgentChaseState : GameObjectFSM
    {
        [Title("Movement Settings")]
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _acceleration = 10f;
        [SerializeField] private float _angularSpeed = 720f;
        [Title("Required Components")]
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private TargetDetector _targetDetector;
        public override void Enter(GameObject entity) {
            _agent.speed = _speed;
            _agent.acceleration = _acceleration;
            _agent.angularSpeed = _angularSpeed;
        }
        public override void Update(GameObject entity) {
            _agent.SetDestination(_targetDetector.Targets[0].transform.position);
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
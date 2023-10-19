using Runtime.Patterns.FSM;
using Runtime.Utils;
using Runtime.Utils.Targetables;
using Tests.Characters.MonsterFSM;
using Tests.Characters.MonsterFSM.StatueStates;
using Tests.Weapons;
using UnityEngine;
using UnityEngine.AI;

namespace Tests.Characters
{
    public class Statue : Character<Statue>
    {
        public float Speed => _speed;
        public float Acceleration => _acceleration;
        public float RotateSpeed => _rotateSpeed;
        public Laser Laser => _laser;
        public NavMeshAgent Controller => _controller;
        public TargetDetector TargetDetector => _targetDetector;
        public TargetLooker TargetLooker => _targetLooker;
        public TargetBlockChecker TargetBlockChecker => _targetBlockChecker;
        public float ShockWaveAttackRange => _shockWaveAttackRange;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _acceleration = 1f;
        [SerializeField] private float _rotateSpeed = 720f;
        [SerializeField] private float _shockWaveAttackRange = 1f;
        [SerializeField] private Laser _laser = null;
        [SerializeField] private NavMeshAgent _controller = null;
        [SerializeField] private TargetDetector _targetDetector = null;
        [SerializeField] private TargetLooker _targetLooker = null;
        [SerializeField] private TargetBlockChecker _targetBlockChecker = null;
        private void Start()
        {
            _controller.speed = _speed;
            _controller.acceleration = _acceleration;
            _controller.angularSpeed = _rotateSpeed;
            State = new StatueIdleState();
        }
    }
}
using Runtime.Utils;
using Runtime.Weapons;
using Tests.Characters.MonsterFSM.UndeadPaladinStates;
using UnityEngine;
using UnityEngine.AI;

namespace Tests.Characters
{
    public class UndeadPaladin : Character<UndeadPaladin>
    {
        public float Speed => _speed;
        public float PatrolSpeed => _patrolSpeed;
        public float Acceleration => _acceleration;
        public float RotateSpeed => _rotateSpeed;
        public NavMeshAgent Controller => _controller;
        public TargetDetector TargetDetector => _targetDetector;
        public TargetLooker TargetLooker => _targetLooker;
        public MeleeWeapon MeleeWeapon => _meleeWeapon;
        public Animator Animator => _animator;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _acceleration = 10f;
        [SerializeField] private float _patrolSpeed = 2f;
        [SerializeField] private float _rotateSpeed = 720f;
        [SerializeField] private MeleeWeapon _meleeWeapon = null;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _controller = null;
        [SerializeField] private TargetDetector _targetDetector = null;
        [SerializeField] private TargetLooker _targetLooker = null;
        private void Start()
        {
            _controller.speed = _speed;
            _controller.acceleration = _acceleration;
            _controller.angularSpeed = _rotateSpeed;
            State = new UndeadPaladinIdleState();
        }
    }
}
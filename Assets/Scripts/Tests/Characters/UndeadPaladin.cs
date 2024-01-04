using Runtime.Utils;
using Runtime.Utils.Components;
using Runtime.Utils.Targetables;
using Tests.Characters.MonsterFSM.UndeadPaladinStates;
using Tests.Weapons;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Tests.Characters
{
    public class UndeadPaladin : Monster<UndeadPaladin>
    {
        public float Speed => _speed;
        public float PatrolSpeed => _patrolSpeed;
        public float Acceleration => _acceleration;
        public float RotateSpeed => _rotateSpeed;
        public NavMeshAgent Controller => _controller;
        public TargetDetector TargetDetector => _targetDetector;
        public TargetLooker TargetLooker => _targetLooker;
        public TargetBlockChecker TargetBlockChecker => _targetBlockChecker;
        public MeleeTestWeapon MeleeTestWeapon => _meleeTestWeapon;
        public Animator Animator => _animator;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _acceleration = 10f;
        [SerializeField] private float _patrolSpeed = 2f;
        [SerializeField] private float _rotateSpeed = 720f;
        [FormerlySerializedAs("_meleeWeapon")] [SerializeField] private MeleeTestWeapon _meleeTestWeapon = null;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _controller = null;
        [SerializeField] private TargetDetector _targetDetector = null;
        [SerializeField] private TargetLooker _targetLooker = null;
        [SerializeField] private TargetBlockChecker _targetBlockChecker = null;
        private void Start()
        {
            _controller.speed = _speed;
            _controller.acceleration = _acceleration;
            _controller.angularSpeed = _rotateSpeed;
            State = new UndeadPaladinIdleState();
        }

        protected override void OnAlive()
        {
            
        }

        protected override void OnDeath()
        {
            
        }
    }
}
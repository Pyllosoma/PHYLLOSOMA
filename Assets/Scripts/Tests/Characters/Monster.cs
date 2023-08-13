using System;
using Runtime.Patterns.FSM;
using Tests.Characters.MonsterFSM;
using Tests.Utils;
using Tests.Weapons;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Tests.Characters
{
    //일단 테스트를 위해 상위 부모 클래스 없이 구현
    public class Monster : MonoBehaviour
    {
        public IState<Monster> State {
            get => _state;
            set {
                _state?.Exit(this);
                _state = value;
                _state?.Enter(this);
            }
        }
        public float Speed => _speed;
        public float Acceleration => _acceleration;
        public float RotateSpeed => _rotateSpeed;
        public Laser Laser => _laser;
        public NavMeshAgent Controller => _controller;
        public TargetDetector TargetDetector => _targetDetector;
        public TargetLooker TargetLooker => _targetLooker;
        private IState<Monster> _state = null;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _acceleration = 1f;
        [SerializeField] private float _rotateSpeed = 720f;
        [SerializeField] private Laser _laser = null;
        [SerializeField] private NavMeshAgent _controller = null;
        [SerializeField] private TargetDetector _targetDetector = null;
        [SerializeField] private TargetLooker _targetLooker = null;
        private void Start()
        {
            _controller.speed = _speed;
            _controller.acceleration = _acceleration;
            _controller.angularSpeed = _rotateSpeed;
            _state = new MonsterIdleState();
        }
        private void Update()
        {
            State?.Update(this);
            if (_targetDetector.IsTargetExist) {
                _targetLooker.SetTarget(_targetDetector.Targets[0].transform);
            } else {
                _targetLooker.SetTarget(null);
            }
        }
    }
}
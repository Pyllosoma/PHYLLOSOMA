using System;
using Runtime.Characters.FSM;
using Tests.Utils;
using UnityEngine;

namespace Tests.Characters
{
    //일단 테스트를 위해 상위 부모 클래스 없이 구현
    public class Monster : MonoBehaviour
    {
        public State<Monster> State {
            get => _state;
            set {
                _state?.Exit(this);
                _state = value;
                _state?.Enter(this);
            }
        }
        public TargetDetector Detector => _targetDetector;
        private State<Monster> _state = null;
        [SerializeField] private TargetDetector _targetDetector = null;
        private void Start()
        {
            _state = new State<Monster>();
        }
        private void Update()
        {
            State?.Execute(this);
        }
    }
}
using System;
using Runtime.Patterns.FSM;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Tests.Characters.MonsterFSM.SkeletonKnightStates
{
    [Serializable]
    public class SkeletonKnightPatrolState : IState<SkeletonKnight>
    {
        public float EnterRange => _enterRange;
        [Header("State Movement Settings")]
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _acceleration = 100f;
        [SerializeField] private float _rotateSpeed = 0f;
        [Header("State Direction Settings")]
        //if distance bigger than this, then go forward
        [SerializeField] private bool _isLateral = false;//앞뒤
        [SerializeField] private bool _isFront = false;//앞
        [SerializeField] private bool _isRight = false;//오른쪽
        [SerializeField] private float _backRange = 1.5f;
        [SerializeField] private float _frontRange = 2.5f;
        [SerializeField] private float _stopRange = 0.1f;
        [SerializeField] private Vector2 _stateTimeRange = new Vector2(2f,3f);
        [SerializeField] private float _stateTime = 0f;
        [Header("State Settings")]
        [SerializeField] private float _enterRange = 10f;
        [SerializeField] private float _violence = 0.5f;
        private float _time = 0f;
        public void Enter(SkeletonKnight entity)
        {
            //값 초기화
            _isLateral = false;
            _isFront = false;
            _isRight = false;
            
            entity.Controller.speed = _speed;
            entity.Controller.acceleration = _acceleration;
            entity.Controller.angularSpeed = _rotateSpeed;
            //get current target
            float distance = entity.TargetDetector.TargetDistance;
            Debug.Log("Distance : " + distance);
            if (distance > _frontRange) {
                _isFront = true;
            }
            else if (distance < _backRange) {
                _isFront = false;
            }
            else {
                //randomly select left or right
                _isLateral = true;
                int random = Random.Range(0,2);
                _isRight = random == 0;
            }
            entity.Animator.SetBool("IsLateral",_isLateral);
            _stateTime = Random.Range(_stateTimeRange.x,_stateTimeRange.y);
        }
        public void Update(SkeletonKnight entity)
        {
            // if (!entity.TargetDetector.IsTargetExist) {
            //     entity.State = entity.IdleState;
            //     return;
            // }
            //Debug.Log("Distance : " + entity.TargetDetector.TargetDistance);
            if (entity.TargetDetector.TargetDistance > _enterRange) {
                entity.State = entity.ChaseState;
                return;
            }
            if (!_isLateral) {
                var distance = entity.TargetDetector.TargetDistance;
                switch (_isFront) {
                    case true when distance < _backRange + _stopRange:
                        entity.State = entity.PatrolState;
                        return;
                    case false when distance > _frontRange - _stopRange:
                        entity.State = entity.PatrolState;
                        return;
                }
            }

            _time += Time.deltaTime;
            Vector3 currentDirection;
            if (_isLateral) {
                currentDirection = _isRight ? entity.transform.right : -entity.transform.right;
            }
            else {
                currentDirection = _isFront ? entity.transform.forward : -entity.transform.forward;
            }

            entity.Controller.SetDestination(currentDirection + entity.transform.position);
            entity.transform.LookAt(entity.TargetDetector.Targets[0].transform.position);
            entity.transform.rotation = Quaternion.Euler(0f,entity.transform.rotation.eulerAngles.y,0f);
            if (_isLateral) {
                entity.Animator.SetFloat("LateralSpeed", _isRight ? entity.Controller.velocity.magnitude : -entity.Controller.velocity.magnitude);
            }
            else {
                entity.Animator.SetFloat("Speed", _isFront ? entity.Controller.velocity.magnitude : -entity.Controller.velocity.magnitude);
            }
            
            if (_time < _stateTime) return;
            //공격 범위 안에 들어와 있을 때에만 계산
            var random = Random.Range(0f, 1f);
            bool isAttack = entity.TargetDetector.TargetDistance < entity.AttackState.AttackRange && random < _violence;
            Debug.Log("random : " + random + " IsAttack : " + isAttack);
            if (isAttack) {
                entity.State = entity.AttackState;
                return;
            }
            entity.State = entity.PatrolState;
            return;

        }
        public void FixedUpdate(SkeletonKnight entity)
        {
        }
        public void Exit(SkeletonKnight entity)
        {
            if (entity.State != this) {
                entity.Animator.SetFloat("LateralSpeed",0f);
                entity.Animator.SetBool("IsLateral",false);
                entity.Controller.ResetPath();
                entity.Controller.velocity = Vector3.zero;
                entity.Controller.angularSpeed = 0f;
                Debug.Log("Reset Path");
            }
            _time = 0f;
        }
    }
}
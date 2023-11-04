using System;
using Runtime.Patterns.FSM;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Tests.Characters.MonsterFSM.SkeletonKnightStates
{
    [Serializable]
    public class SkeletonKnightWaitState : IState<SkeletonKnight>
    {
        [SerializeField] private float _orbitRange = 1f;
        [SerializeField] private float _orbitExitRange = 3f;
        [SerializeField] private Vector2 _orbitAngleRange  = new Vector2(-180f,180f);
        [SerializeField] private Vector2 _orbitTimeRange = new Vector2(2f,3f);
        [SerializeField] private float _violence = 0.5f;
        private float _curOrbitAngle = 1f;
        private float _curOrbitTime = 1f;
        private float _time = 0f;
        private float _angleSpeed = 0f;
        private float _startAngle = 0f;
        private float _curAngle = 0f;
        private float _endAngle = 0f;
        public void Enter(SkeletonKnight entity)
        {
            entity.Animator.SetBool("IsLateral",true);
            var targetPos = entity.TargetDetector.Targets[0].transform.position;
            var selfPos = entity.transform.position;
            var norDir = new Vector2(targetPos.x - selfPos.x, targetPos.z - selfPos.z).normalized;
            _curOrbitTime = Random.Range(_orbitTimeRange.x,_orbitTimeRange.y);
            _curOrbitAngle = Random.Range(_orbitAngleRange.x,_orbitAngleRange.y);
            _angleSpeed = _curOrbitAngle / _curOrbitTime;
            _startAngle = Mathf.Atan2(norDir.y,norDir.x) * Mathf.Rad2Deg;
            _curAngle = _startAngle;
            
        }
        public void Update(SkeletonKnight entity)
        {
            if (!entity.TargetDetector.IsTargetExist) return;
            //Debug.Log("Distance : " + entity.TargetDetector.TargetDistance);
            if (entity.TargetDetector.TargetDistance > _orbitExitRange) {
                entity.State = entity.ChaseState;
                return;
            }
            _time += Time.deltaTime;
            var curAngleSpeed = _angleSpeed * Time.deltaTime;
            entity.Animator.SetFloat("LateralSpeed", curAngleSpeed);
            _curAngle += curAngleSpeed;
            var targetPos = entity.TargetDetector.Targets[0].transform.position;
            var curPosDir = new Vector2(Mathf.Sin(_curAngle * Mathf.Deg2Rad),Mathf.Cos(_curAngle * Mathf.Deg2Rad));
            var curPos = new Vector3(curPosDir.x,0f,curPosDir.y) * _orbitRange + targetPos;
            
            Debug.Log($"Start angle : {_startAngle}, End angle : {_endAngle}, Cur angle : {_curAngle}, Cur pos : {curPos}");
            //Debug.Log("Cur pos : " + curPos);
            entity.Controller.SetDestination(curPos);
            entity.transform.LookAt(targetPos);
            entity.transform.rotation = Quaternion.Euler(0f,entity.transform.rotation.eulerAngles.y,0f);
            entity.Animator.SetFloat("Speed", entity.Controller.velocity.magnitude);

            
            if (_time > _curOrbitTime) {
                var stateRatio = Random.Range(0f,1f);
                if (stateRatio < _violence) {
                    entity.State = entity.AttackState;
                    return;
                }
                entity.State = entity.ChaseState;
                return;
            }
            
        }
        public void FixedUpdate(SkeletonKnight entity)
        {
        }
        public void Exit(SkeletonKnight entity)
        {
            entity.Animator.SetFloat("LateralSpeed",0f);
            entity.Animator.SetBool("IsLateral",false);
            entity.Controller.ResetPath();
            _time = 0f;
            _angleSpeed = 0f;
            _startAngle = 0f;
            _curAngle = 0f;
            _endAngle = 0f;
        }
    }
}
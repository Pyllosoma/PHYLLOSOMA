using System;
using Runtime.Patterns.FSM;
using UnityEngine;
namespace Tests.Characters.MonsterFSM.StatueStates
{
    [Serializable]
    public class StatueLaserAttackState : IState<Statue>
    {
        [SerializeField] private float _startTime = 0f;
        [SerializeField] private float _chargeTime = 2f;
        [SerializeField] private float _attackTime = 1f;
        [SerializeField] private Vector3 _targetDirection = Vector3.zero;
        public void Enter(Statue entity)
        {
            entity.Laser.Ready();
            _startTime = Time.time;
            _targetDirection = entity.TargetDetector.Targets[0].transform.position - entity.Laser.transform.position;
        }
        public void Update(Statue entity)
        {
            //충전 이펙트
            if (_startTime + _chargeTime > Time.time) {
                //entity.Laser.Charge();
                return;
            }
            //공격 이펙트
            if (_startTime + _chargeTime + _attackTime > Time.time) {
                //make raycast by using direction
                Debug.DrawRay(entity.Laser.transform.position, _targetDirection, Color.red);
                if (Physics.Raycast(entity.Laser.transform.position, _targetDirection, out RaycastHit hit)) {
                    entity.Laser.Attack(hit.collider.gameObject, hit.point);
                    return;
                }
                //if hit collider, then shot laser
                
                return;
            }
            entity.State = new StatueIdleState();
        }
        public void FixedUpdate(Statue entity)
        {
            
        }
        public void Exit(Statue entity)
        {
            entity.Laser.Finish();
        }
    }
}
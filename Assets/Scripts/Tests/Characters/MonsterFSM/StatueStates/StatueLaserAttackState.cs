using Runtime.Patterns.FSM;
using UnityEngine;
namespace Tests.Characters.MonsterFSM.StatueStates
{
    public class StatueLaserAttackState : IState<Statue>
    {
        private float _startTime = 0f;
        private float _chargeTime = 2f;
        private float _attackTime = 1f;
        private Vector3 _targetDirection = Vector3.zero;
        public void Enter(Statue entity)
        {
            entity.Laser.Ready();
            _startTime = UnityEngine.Time.time;
            _targetDirection = entity.TargetDetector.Targets[0].transform.position - entity.transform.position;
        }
        public void Update(Statue entity)
        {
            //충전 이펙트
            if (_startTime + _chargeTime < UnityEngine.Time.time) {
                //entity.Laser.Charge();
                return;
            }
            //공격 이펙트
            if (_startTime + _chargeTime + _attackTime < Time.time) {
                //make raycast by using direction
                Physics.Raycast(entity.Laser.transform.position, entity.Laser.transform.position+_targetDirection, out RaycastHit hit);
                if (!hit.collider) {
                    return;
                }
                //if hit collider, then shot laser
                entity.Laser.Attack(hit.collider.gameObject, hit.point);
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
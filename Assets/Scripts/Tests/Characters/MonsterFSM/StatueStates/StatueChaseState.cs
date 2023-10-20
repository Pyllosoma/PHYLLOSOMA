using System;
using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.StatueStates
{
    [Serializable] 
    public class StatueChaseState : IState<Statue>
    {
        [SerializeField] private float _chaseSpeed = 10f;
        [SerializeField] private float _acceleration = 10f;
        [SerializeField] private float _rotateSpeed = 720f;
        
        public void Enter(Statue entity)
        {
            //값에 따라 초기화
            entity.Controller.speed = _chaseSpeed;
            entity.Controller.acceleration = _acceleration;
            entity.Controller.angularSpeed = _rotateSpeed;
            
            entity.TargetLooker.SetTarget(entity.TargetDetector.Targets[0].transform);
            entity.TargetBlockChecker.SetTarget(entity.TargetDetector.Targets[0].transform);
        }
        public void Update(Statue entity)
        {
            if (!entity.TargetDetector.IsTargetExist) {
                entity.State = entity.IdleState;
                return;
            }
            var target = entity.TargetDetector.Targets[0];
            if (entity.Laser.IsInRange(Vector3.Distance(entity.gameObject.transform.position, entity.TargetBlockChecker.TargetPosition))) {
                entity.State = entity.AttackState;
                return;
            }
            entity.Controller.SetDestination(target.transform.position);
        }
        public void FixedUpdate(Statue entity)
        {
            
        }
        public void Exit(Statue entity)
        {
            //캐릭터를 멈추도록 만든다
            entity.Controller.ResetPath();
            entity.Controller.velocity = Vector3.zero;
        }
    }
}
using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.UndeadPaladinStates
{
    public class UndeadPaladinIdleState : IState<UndeadPaladin>
    {
        private float _currentIdleStateTime = 10f;
        private float _patrolChanceUpdateRatio = 1f;
        private float _patrolChanceTime = 0f;
        private float _startTime = 0f;
        
        public void Enter(UndeadPaladin entity)
        {
            _startTime = Time.time;
            _patrolChanceTime = Time.time;
            entity.Animator.SetBool("IsDefaultIdle" , true);
            
            entity.TargetLooker.SetTarget(null);
            entity.Animator.SetFloat("Speed", 0f);

        }
        public void Update(UndeadPaladin entity)
        {
            if (entity.TargetDetector.IsTargetExist) {
                entity.State = new UndeadPaladinChasePattern();
                return;
            }
            if (Time.time - _patrolChanceTime > _patrolChanceUpdateRatio) {
                _patrolChanceTime = Time.time;
                int random = Random.Range(0, 100);
                //Patrol with % chance
                if (random < 10) {
                    entity.State = new UndeadPaladinPatrolState();
                    return;
                }
            }
            if (Time.time - _startTime > _currentIdleStateTime) {
                entity.State = new UndeadPaladinOffensiveIdleState();
                return;
            }
        }
        public void FixedUpdate(UndeadPaladin entity)
        {

        }
        public void Exit(UndeadPaladin entity)
        {

        }
    }
}
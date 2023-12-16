using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.SkeletonKnightStates
{
    public class SkeletonKnightGroggyState : IState<SkeletonKnight>
    {
        [SerializeField] private float _groggyTime = 3f;
        private float _timer = 0f;
        public void Enter(SkeletonKnight entity)
        {
            entity.Animator.SetTrigger("Groggy");
            _timer = 0f;
        }
        public void Update(SkeletonKnight entity)
        {
            _timer += Time.deltaTime;
            if (_timer >= _groggyTime) {
                entity.State = entity.IdleState;
            }
        }
        public void FixedUpdate(SkeletonKnight entity) { }
        public void Exit(SkeletonKnight entity)
        {
            
        }
    }
}
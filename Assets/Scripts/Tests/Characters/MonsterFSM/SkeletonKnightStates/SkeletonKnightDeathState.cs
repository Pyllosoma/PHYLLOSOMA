using Runtime.Patterns.FSM;

namespace Tests.Characters.MonsterFSM.SkeletonKnightStates
{
    public class SkeletonKnightDeathState : IState<SkeletonKnight>
    {
        
        public void Enter(SkeletonKnight entity)
        {
            entity.Animator.SetTrigger("Death");
        }
        public void Update(SkeletonKnight entity)
        {
            
        }
        public void FixedUpdate(SkeletonKnight entity)
        {
            
        }
        public void Exit(SkeletonKnight entity)
        {
            
        }
    }
}
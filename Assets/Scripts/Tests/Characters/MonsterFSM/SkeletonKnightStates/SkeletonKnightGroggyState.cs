using System;
using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.SkeletonKnightStates
{
    [Serializable]
    public class SkeletonKnightGroggyState : IState<SkeletonKnight>
    {
        [SerializeField] private float _groggyTime = 4f;
        private float _timer = 0f;
        public void Enter(SkeletonKnight entity)
        {
            entity.Animator.SetTrigger("Groggy");
            entity.SoulComponent.EnableSoul = false;
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
            if (entity.HealthComponent.Health > 0f) {
                entity.SoulComponent.Reset();
            }
        }
    }
}
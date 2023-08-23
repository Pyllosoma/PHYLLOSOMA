﻿using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.UndeadPaladinStates
{
    public class UndeadPaladinAttackState : IState<UndeadPaladin>
    {
        public void Enter(UndeadPaladin entity)
        {
            //Debug.Log("UndeadPaladinAttackState Enter");
            entity.Animator.SetBool("IsAttack", true);
            //Debug.Log(entity.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
            //Debug.Log();
            entity.MeleeWeapon.SetOwner(entity.gameObject).SetWaitTime(0.5f).SetDelay(0.75f).SetDelay(2.4f).Ready();
        }
        public void Update(UndeadPaladin entity)
        {
            if (!entity.TargetDetector.IsTargetExist) {
                entity.State = new UndeadPaladinIdleState();
                return;
            }
            if (!entity.MeleeWeapon.IsInRange(Vector3.Distance(entity.gameObject.transform.position, entity.TargetBlockChecker.TargetPosition))) {
                entity.State = new UndeadPaladinChasePattern();
                return;
            }
        }
        public void FixedUpdate(UndeadPaladin entity)
        {
            
        }
        public void Exit(UndeadPaladin entity)
        {
            entity.Animator.SetBool("IsAttack", false);
            entity.MeleeWeapon.Finish();
        }
    }
}
﻿using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.UndeadPaladinStates
{
    public class UndeadPaladinChasePattern : IState<UndeadPaladin>
    {

        public void Enter(UndeadPaladin entity)
        {
            entity.Animator.transform.localPosition = Vector3.zero;
            entity.Animator.transform.localRotation = Quaternion.identity;
            entity.Animator.applyRootMotion = false;
            entity.Animator.SetFloat("Speed",0.1f);
            entity.TargetLooker.SetTarget(entity.TargetDetector.Targets[0].transform);
            entity.TargetBlockChecker.SetTarget(entity.TargetDetector.Targets[0].transform);
        }
        public void Update(UndeadPaladin entity)
        {
            if (!entity.TargetDetector.IsTargetExist) {
                entity.State = new UndeadPaladinIdleState();
                return;
            }
            var target = entity.TargetDetector.Targets[0].transform;
            if (entity.MeleeTestWeapon.IsInRange(Vector3.Distance(entity.gameObject.transform.position, target.position))) {
                entity.State = new UndeadPaladinAttackState();
                return;
            }
            entity.Controller.SetDestination(target.position);
            entity.Animator.SetFloat("Speed", entity.Controller.velocity.magnitude);
        }
        public void FixedUpdate(UndeadPaladin entity)
        {
        }
        public void Exit(UndeadPaladin entity)
        {
            entity.Controller.velocity = Vector3.zero;
            entity.Animator.applyRootMotion = true;
            entity.Animator.SetFloat("Speed",0);
            entity.Controller.ResetPath();
        }
    }
}
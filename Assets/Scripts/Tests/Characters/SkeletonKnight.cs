using System;
using Runtime.Patterns.FSM;
using Runtime.Utils;
using Runtime.Utils.Targetables;
using Tests.Characters.MonsterFSM.SkeletonKnightStates;
using Tests.Weapons;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Tests.Characters
{
    public class SkeletonKnight : Monster<SkeletonKnight>
    {
        public SkeletonKnightIdleState IdleState = new SkeletonKnightIdleState();
        public SkeletonKnightChaseState ChaseState = new SkeletonKnightChaseState();
        public SkeletonKnightAttackState AttackState = new SkeletonKnightAttackState();
        public SkeletonKnightDeathState DeathState = new SkeletonKnightDeathState();
        public SkeletonKnightReturnState ReturnState = new SkeletonKnightReturnState();
        public SkeletonKnightPatrolState PatrolState = new SkeletonKnightPatrolState();
        public NavMeshAgent Controller => _controller;
        public TargetDetector TargetDetector => _targetDetector;
        public MeleeTestWeapon MeleeWeapon => _meleeWeapon;
        public Vector3 SpawnPosition => _spawnPosition;
        public Animator Animator => _animator;
        [SerializeField] private MeleeTestWeapon _meleeWeapon = null;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _controller = null;
        [SerializeField] private TargetDetector _targetDetector = null;
        private Vector3 _spawnPosition;
        private void Start()
        {
            _spawnPosition = transform.position;
            State = IdleState;
        }
    }
}
using System;
using Runtime.Utils;
using Runtime.Utils.Targetables;
using Runtime.Weapons;
using Tests.Characters.MonsterFSM.SkeletonKnightStates;
using UnityEngine;
using UnityEngine.AI;

namespace Tests.Characters
{
    public class SkeletonKnight : Monster<SkeletonKnight>
    {
        public SkeletonKnightIdleState IdleState = new SkeletonKnightIdleState();
        public SkeletonKnightChaseState ChaseState = new SkeletonKnightChaseState();
        public SkeletonKnightAttackState AttackState = new SkeletonKnightAttackState();
        public SkeletonKnightDeathState DeathState = new SkeletonKnightDeathState();
        public SkeletonKnightReturnState ReturnState = new SkeletonKnightReturnState();
        public NavMeshAgent Controller => _controller;
        public TargetDetector TargetDetector => _targetDetector;
        public TargetLooker TargetLooker => _targetLooker;
        public TargetBlockChecker TargetBlockChecker => _targetBlockChecker;
        public MeleeWeapon MeleeWeapon => _meleeWeapon;
        public Vector3 SpawnPosition => _spawnPosition;
        public Animator Animator => _animator;
        [SerializeField] private MeleeWeapon _meleeWeapon = null;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _controller = null;
        [SerializeField] private TargetDetector _targetDetector = null;
        [SerializeField] private TargetLooker _targetLooker = null;
        [SerializeField] private TargetBlockChecker _targetBlockChecker = null;
        private Vector3 _spawnPosition;
        private void Start()
        {
            _spawnPosition = transform.position;
            State = IdleState;
        }
    }
}
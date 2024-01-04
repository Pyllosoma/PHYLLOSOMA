using System;
using Runtime.Characters.Components;
using Runtime.Characters.Interfaces;
using Runtime.Patterns.FSM;
using Runtime.Utils;
using Runtime.Utils.Components;
using Runtime.Utils.Targetables;
using Sirenix.OdinInspector;
using Tests.Characters.MonsterFSM.SkeletonKnightStates;
using Tests.Weapons;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Tests.Characters
{
    public class SkeletonKnight : Monster<SkeletonKnight>
    {
        [Header("Skeleton Knight States")]
        public SkeletonKnightIdleState IdleState = new SkeletonKnightIdleState();
        public SkeletonKnightChaseState ChaseState = new SkeletonKnightChaseState();
        public SkeletonKnightAttackState AttackState = new SkeletonKnightAttackState();
        public SkeletonKnightDeathState DeathState = new SkeletonKnightDeathState();
        public SkeletonKnightGroggyState GroggyState = new SkeletonKnightGroggyState();
        public SkeletonKnightPatrolState PatrolState = new SkeletonKnightPatrolState();
        public NavMeshAgent Controller => _controller;
        public TargetDetector TargetDetector => _targetDetector;
        public MeleeTestWeapon MeleeWeapon => _meleeWeapon;
        public Vector3 SpawnPosition => _spawnPosition;
        public Animator Animator => _animator;
        [Header("Skeleton Knight")]
        [SerializeField] private MeleeTestWeapon _meleeWeapon = null;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _controller = null;
        [SerializeField] private TargetDetector _targetDetector = null;
        [Title("Character Components")]
        public SoulComponent SoulComponent => _soulComponent;
        public HealthComponent HealthComponent => _healthComponent;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private SoulComponent _soulComponent;

        private Vector3 _spawnPosition;
        public void Groggy()
        {
            State = GroggyState;
        }
        public override void Start()
        {
            base.Start();
            _spawnPosition = transform.position;
            State = IdleState;
        }

        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Space)) {
                _healthComponent.GiveDamage(100f);
                _soulComponent.GiveSoulDamage(50f);
            }
        }
        protected override void OnAlive()
        {
            _healthComponent.Reset();
            _soulComponent.Reset();
        }
        protected override void OnDeath()
        {
            State = DeathState;
        }

    }
}
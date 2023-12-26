using System;
using Runtime.Characters.Components;
using Runtime.Characters.Interfaces;
using Runtime.Patterns.FSM;
using Runtime.Utils;
using Runtime.Utils.Targetables;
using Sirenix.OdinInspector;
using Tests.Characters.MonsterFSM.SkeletonKnightStates;
using Tests.UI;
using Tests.Weapons;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Tests.Characters
{
    public class SkeletonKnight : Monster<SkeletonKnight>,IHealth
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
        [Header("Status")]
        [SerializeField] private float _maxHealth = 100;
        [SerializeField] private float _health = 100;
        [SerializeField] private float _maxSoulGauge = 100;
        [SerializeField] private float _soulGauge = 100;
        [Header("UI")]
        [SerializeField] private MonsterUI _monsterUI = null;
        
        [Title("Character Components")]
        public SoulComponent SoulComponent => _soulComponent;
        [SerializeField] private SoulComponent _soulComponent;
        private Vector3 _spawnPosition;
        #region IHealth
        public float GiveDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0) {
                _health = 0f;
                Death();
            }
            if (_health >= _maxHealth) {
                _health = _maxHealth;
            }
            _monsterUI.SetHealth(_health);
            return _health;
        }
        public float GetHealth() => _health;
        public void ResetHealth() => _health = _maxHealth;
        #endregion
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
                GiveDamage(5f);
                _soulComponent.GiveSoulDamage(50f);
            }
        }
        protected override void OnAlive()
        {
            ResetHealth();
        }

        protected override void OnDeath()
        {
            State = DeathState;
        }

    }
}
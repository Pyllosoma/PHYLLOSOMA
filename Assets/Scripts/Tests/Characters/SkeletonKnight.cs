using System;
using Runtime.Characters.Interfaces;
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
    public class SkeletonKnight : Monster<SkeletonKnight>,IHealth,ISoul
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
        #region ISoul
        public float GiveSoulDamage(float damage)
        {
            _soulGauge -= damage;
            if (_soulGauge <= 0) {
                _soulGauge = 0f;
                Groggy();
            }
            if (_soulGauge >= _maxSoulGauge) {
                _soulGauge = _maxSoulGauge;
            }
            _monsterUI.SetSoulGauge(_soulGauge);
            return _soulGauge;
        }
        public float GetSoulGauge() => _soulGauge;
        public void ResetSoulGauge() => _soulGauge = _maxSoulGauge;
        public void Groggy()
        {
            State = GroggyState;
        }
        #endregion
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
                GiveDamage(50f);
                GiveSoulDamage(1f);
            }
        }
        protected override void OnAlive()
        {
            ResetHealth();
            ResetSoulGauge();
        }

        protected override void OnDeath()
        {
            State = DeathState;
        }

    }
}
using Runtime.Patterns.FSM;
using Runtime.Utils;
using Runtime.Utils.Targetables;
using Tests.Characters.MonsterFSM;
using Tests.Characters.MonsterFSM.StatueStates;
using Tests.Weapons;
using UnityEngine;
using UnityEngine.AI;

namespace Tests.Characters
{
    public class Statue : Monster<Statue>
    {
        public NavMeshAgent Controller => _controller;
        public TargetDetector TargetDetector => _targetDetector;
        public TargetLooker TargetLooker => _targetLooker;
        public TargetBlockChecker TargetBlockChecker => _targetBlockChecker;
        public float LaserRange => _laserRange;
        [SerializeField] private float _laserRange = 1f;
        [SerializeField] private NavMeshAgent _controller = null;
        [SerializeField] private TargetDetector _targetDetector = null;
        [SerializeField] private TargetLooker _targetLooker = null;
        [SerializeField] private TargetBlockChecker _targetBlockChecker = null;
        public StatueIdleState IdleState => _idleState;
        public StatueChaseState ChaseState => _chaseState;
        public StatueAttackState AttackState => _attackState;
        public StatueLaserAttackState LaserAttackState => _laserAttackState;
        [Header("Statue States")]
        [SerializeField] private StatueIdleState _idleState = new StatueIdleState();
        [SerializeField] private StatueChaseState _chaseState = new StatueChaseState();
        [SerializeField] private StatueAttackState _attackState = new StatueAttackState();
        [SerializeField] private StatueLaserAttackState _laserAttackState = new StatueLaserAttackState();
        [SerializeField] private StatueShockWaveAttackState _shockWaveAttackState = new StatueShockWaveAttackState();
        private void Start()
        {
            State = _idleState;
        }

        protected override void OnAlive()
        {
            
        }

        protected override void OnDeath()
        {
            
        }
    }
}
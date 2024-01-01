using Runtime.Characters;
using Runtime.Patterns.FSM;
using Runtime.Utils;
using Runtime.Utils.Targetables;
using Sirenix.OdinInspector;
using Tests.Characters.FSM;
using Tests.Characters.MonsterFSM;
using Tests.Characters.MonsterFSM.StatueStates;
using Tests.Weapons;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Tests.Characters
{
    public class Statue : Monster<Character>
    {
        [Title("Statue States")]
        [SerializeReference] private GameObjectFSM _idle;
        [SerializeReference] private GameObjectFSM _chase;
        [SerializeReference] private GameObjectFSM _homingMissileAttack;
        [SerializeReference] private GameObjectFSM _chainAttack;
        [SerializeReference] private GameObjectFSM _laserAttack;
        [SerializeReference] private GameObjectFSM _shockWaveAttack;
        [SerializeReference] private GameObjectFSM _death;
        //Create Test State Mahcine
        public GameObjectFSM TestState
        {
            get => _testState;
            set
            {
                _testState?.Exit(gameObject);
                _testState = value;
                _testState?.Enter(gameObject);
            }
        }
        private GameObjectFSM _testState;
        public override void Start()
        {
            base.Start();            
            _testState = _idle;
        }
        protected override void Update()
        {
            base.Update();
            _testState?.Update(gameObject);
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _testState?.FixedUpdate(gameObject);
        }
        
        protected override void OnAlive()
        {
            
        }
        protected override void OnDeath()
        {
            TestState = _death;
        }
        public void HomingMissileAttack()
        {
            TestState = _homingMissileAttack;
        }
        public void ChainAttack()
        {
            TestState = _chainAttack;
        }
        public void LaserAttack()
        {
            TestState = _laserAttack;
        }
        public void ShockWaveAttack()
        {
            TestState = _shockWaveAttack;
        } 
        public void Chase()
        {
            TestState = _chase;
        }
        public void Idle()
        {
            TestState = _idle;
        }
    }
}
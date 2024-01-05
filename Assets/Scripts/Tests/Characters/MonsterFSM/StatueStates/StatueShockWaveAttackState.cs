using System;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Characters.FSM;
using Runtime.Patterns.FSM;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Tests.Characters.MonsterFSM.StatueStates
{
    [Serializable]
    public class StatueShockWaveAttackState : GameObjectFSM
    {
        [Title("State Settings")]
        [SerializeField] private float _timer = 0f;
        [SerializeField] private float _chargeTime = .5f;
        [SerializeField] private float _attackTime = 1f;
        [SerializeField] private float _returnTime = 1f;
        [Title("Offset Settings")]
        [SerializeField] private Vector3 _flyOffset = new Vector3(0,1,0);
        [SerializeField] private Vector3 _groundPoundOffset = new Vector3(0,-1,0);
        [FoldoutGroup("Result Positions")]
        [SerializeField] private Vector3 _flyPosition;
        [FoldoutGroup("Result Positions")]
        [SerializeField] private Vector3 _groundPoundPosition;
        [Title("Required Components")]
        [SerializeField] private GameObject _target;
        [FoldoutGroup("Shock Wave Attack Events")]
        [SerializeField] private UnityEvent _onShockWaveEnd;

        public override void Enter(GameObject entity)
        {
            base.Enter(entity);
            Debug.Log("ShockWaveAttack Enter");
            _timer = 0f;
            _flyPosition = _target.transform.localPosition + _flyOffset;
            if (Physics.Raycast(_target.transform.position, Vector3.down, out RaycastHit hit)) {
                _groundPoundPosition = hit.point - _target.transform.position + _groundPoundOffset;
            }
            GroundPoundCharge();
        }
        private void GroundPoundCharge() {
            //Debug.Log("GroundPound");
            _target
                    .transform
                    .DOLocalMove(_flyPosition, _chargeTime)
                    .SetEase(Ease.Linear)
                    .OnComplete(GroundPoundStart);
        }
        private void GroundPoundStart() {
            //Debug.Log("GroundPoundStart");
            _target
                    .transform
                    .DOLocalMove(_groundPoundPosition, _attackTime)
                    .SetEase(Ease.InSine)
                    .OnComplete(()=>
                    {
                        _target.transform.DOKill();
                        _target.transform.localPosition = _groundPoundPosition;
                        _onShockWaveEnd?.Invoke();
                    });
        }
    }
}
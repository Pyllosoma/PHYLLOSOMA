using System;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Characters.FSM;
using Runtime.Patterns.FSM;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField] private Vector3 _groundPound = new Vector3(0,-1,0);
        [SerializeField] private Vector3[] _animationPosition = new Vector3[3];
        [Title("Required Components")]
        [SerializeField] private GameObject _model;
        [FoldoutGroup("Shock Wave Attack Events")]
        [SerializeField] private UnityEvent _onShockWaveEnd;

        public override void Enter(GameObject entity)
        {
            base.Enter(entity);
            Debug.Log("ShockWaveAttack Enter");
            _timer = 0f;
            _animationPosition[0] = _model.transform.localPosition + _flyOffset;
            if (Physics.Raycast(_model.transform.position, Vector3.down, out RaycastHit hit)) {
                _animationPosition[1] = hit.point - entity.transform.position + _groundPound;
            }
            _animationPosition[2] = _model.transform.localPosition;
            GroundPoundCharge();
        }
        private void GroundPoundCharge() {
            Debug.Log("GroundPound");
            _model
                    .transform
                    .DOLocalMove(_animationPosition[0], _chargeTime)
                    .SetEase(Ease.Linear)
                    .onComplete += GroundPoundStart;
        }
        private void GroundPoundStart() {
            Debug.Log("GroundPoundStart");
            _model
                    .transform
                    .DOLocalMove(_animationPosition[1], _attackTime)
                    .SetEase(Ease.InSine)
                    .onComplete += GroundPoundEnd;
        }
        private void GroundPoundEnd() {
            Debug.Log("GroundPoundEnd");
            _model
                    .transform
                    .DOLocalMove(_animationPosition[2], _returnTime)
                    .SetEase(Ease.OutBack)
                    .onComplete += () => {
                        _model.transform.DOKill();
                        _model.transform.localPosition = _animationPosition[2];
                        _onShockWaveEnd?.Invoke();
                    };
        }
    }
}
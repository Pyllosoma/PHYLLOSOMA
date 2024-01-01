using System;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.StatueStates
{
    [Serializable]
    public class StatueShockWaveAttackState : IState<Statue>
    {
        //private List<GameObject> _targets = new List<GameObject>();
        [SerializeField] private float _timer = 0f;
        [SerializeField] private float _chargeTime = .5f;
        [SerializeField] private float _attackTime = 1f;
        [SerializeField] private float _returnTime = 1f;
        [SerializeField] private Vector3 _flyOffset = new Vector3(0,1,0);
        [SerializeField] private Vector3[] _animationPosition = new Vector3[3];
        [SerializeField] private GameObject _model;
        [SerializeField] private int _currentAnimation = 0;
        public void Enter(Statue entity)
        {
            _timer = 0f;
            _animationPosition[0] = _model.transform.localPosition + _flyOffset;
            if (Physics.Raycast(_model.transform.position, Vector3.down, out RaycastHit hit)) {
                _animationPosition[1] = hit.point - entity.transform.position - new Vector3(0,1,0);
            }
            _animationPosition[2] = _model.transform.localPosition;
            _currentAnimation = -1;
        }
        public void Update(Statue entity)
        {
            _timer += Time.deltaTime;
            
            //충전 이펙트
            if (_timer > _chargeTime && _currentAnimation < 0) {
                _currentAnimation = 0;
                _model.transform.DOLocalMove(_animationPosition[0], _chargeTime).SetEase(Ease.Linear);
                return;
            }
            //찍기 이펙트
            if (_timer > _chargeTime + _attackTime && _currentAnimation < 1) {
                _currentAnimation = 1;
                _model.transform.DOLocalMove(_animationPosition[1], _attackTime).SetEase(Ease.InSine);
                return;
            }
            //돌아오기 이펙트
            if (_timer > _chargeTime + _attackTime + _returnTime && _currentAnimation < 2) {
                _currentAnimation = 2;
                _model.transform.DOLocalMove(_animationPosition[2], _returnTime).SetEase(Ease.OutBack).onComplete += () => {
                    _model.transform.DOKill();
                    _model.transform.localPosition = _animationPosition[2];
                    entity.State = entity.IdleState;
                };
                return;
            }
        }
        public void FixedUpdate(Statue entity)
        {
            
        }
        public void Exit(Statue entity)
        {
            //entity.ShockWave.Finish();
        }
    }
}
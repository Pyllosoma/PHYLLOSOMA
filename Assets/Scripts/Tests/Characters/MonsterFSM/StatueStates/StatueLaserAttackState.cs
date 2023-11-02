using System;
using DG.Tweening;
using Runtime.Patterns.FSM;
using UnityEngine;
using UnityEngine.VFX;

namespace Tests.Characters.MonsterFSM.StatueStates
{
    [Serializable]
    public class StatueLaserAttackState : IState<Statue>
    {
        [SerializeField] private GameObject _muzzle;
        [SerializeField] private LineRenderer _laser;
        [SerializeField] private VisualEffect _laserHit;
        [SerializeField] private float _time = 0f;
        [SerializeField] private float _chargeTime = 2f;
        [SerializeField] private float _attackTime = 1f;
        [Header("Muzzle Animation Setting")]
        [SerializeField] private Vector2 _muzzleSizeOffset = new Vector2(2f,1f);
        [SerializeField] private AnimationCurve _muzzleAnimationCurve = AnimationCurve.Linear(0f,0f,1f,1f);
        [SerializeField] private Vector3 _targetDirection = Vector3.zero;
        public void Enter(Statue entity)
        {
            _time = 0f;
            _muzzle.transform.DOScale(_muzzleSizeOffset.y, 0.1f).SetEase(Ease.OutBack);
            _targetDirection = entity.TargetDetector.Targets[0].transform.position - _muzzle.transform.position;
        }
        public void Update(Statue entity)
        {
            _time += Time.deltaTime;
            //충전 이펙트
            if (_chargeTime > _time) {
                var scale = _muzzleAnimationCurve.Evaluate(_time / _chargeTime) * (_muzzleSizeOffset.y - _muzzleSizeOffset.x) + _muzzleSizeOffset.x;
                _muzzle.transform.localScale = new Vector3(scale,scale, scale);
                return;
            }
            //공격 이펙트
            if (_chargeTime + _attackTime > _time) {
                //make raycast by using direction
                Debug.DrawRay(_muzzle.transform.position, _targetDirection, Color.red);
                if (Physics.Raycast(_muzzle.transform.position, _targetDirection, out RaycastHit hit)) {
                    _laserHit.gameObject.SetActive(true);
                    _laser.gameObject.SetActive(true);
                    _laser.SetPosition(0, _muzzle.transform.position);
                    _laser.SetPosition(1, hit.point);
                    _laserHit.transform.position = hit.point;
                    
                    
                    return;
                }
                //if hit collider, then shot laser
                
                return;
            }
            entity.State = new StatueIdleState();
        }
        public void FixedUpdate(Statue entity)
        {
            
        }
        public void Exit(Statue entity)
        {
            _laser.gameObject.SetActive(false);
            _laserHit.gameObject.SetActive(false);
        }
    }
}
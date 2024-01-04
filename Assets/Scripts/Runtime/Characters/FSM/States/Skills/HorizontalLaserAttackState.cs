using System;
using DG.Tweening;
using Runtime.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

namespace Runtime.Characters.FSM.States.Skills
{
    [Serializable]
    public class HorizontalLaserAttackState : TargetBaseState
    {
        [Title("Laser Attack Settings")]
        [SerializeField] private float _time = 0f;
        [SerializeField] private float _chargeTime = 1f;
        [SerializeField] private float _attackTime = 1f;
        [SerializeField] private float _laserLength = 3f;
        [SerializeField] private Vector3 _laserStartDirection = Vector3.zero;
        [SerializeField] private Vector3 _laserEndDirection = Vector3.zero;
        [Header("Muzzle Animation Setting")]
        [SerializeField] private Vector2 _muzzleSizeOffset = new Vector2(0.3f,0.15f);
        [SerializeField] private AnimationCurve _muzzleAnimationCurve = AnimationCurve.Linear(0f,0f,1f,1f);
        [Title("Required Components")]
        [SerializeField] private Transform _muzzle;
        [SerializeField] private LineRenderer _laser;
        [SerializeField] private VisualEffect _laserHit;
        [FoldoutGroup("Laser Attack Events")]
        [SerializeField] private UnityEvent _onLaserEnd;
        public override void Enter(GameObject entity)
        {
            base.Enter(entity);
            _time = 0f;
            _muzzle.transform.DOScale(_muzzleSizeOffset.y, 0.1f).SetEase(Ease.OutBack);
            //calculate laser start and end direction
            var targetDirection = _targetableComponent.Target.transform.position - _muzzle.position;
            targetDirection.Normalize();
            //calculate laser start and end direction
            var right = Vector3.Cross(Vector3.up, targetDirection);
            var left = -right;
            //calculate laser start and end position
            var target = _targetableComponent.Target.transform.position;
            var leftDirectionPos = target + left * _laserLength;
            var rightDirectionPos = target + right * _laserLength;
            //calculate laser start and end direction
            _laserStartDirection = leftDirectionPos - _muzzle.position;
            _laserEndDirection = rightDirectionPos - _muzzle.position;
            //Normalize direction
            _laserStartDirection.Normalize();
            _laserEndDirection.Normalize();
        }

        public override void Update(GameObject entity)
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
                var currentPercent = (_time - _chargeTime) / _attackTime;
                //make raycast by using direction
                //Debug.DrawRay(_muzzle.transform.position, _targetDirection, Color.red);
                var targetDirection = Vector3.Lerp(_laserStartDirection, _laserEndDirection, currentPercent);
                //Debug.DrawRay(_muzzle.transform.position, targetDirection, Color.red);
                if (Physics.Raycast(_muzzle.transform.position, targetDirection, out RaycastHit hit)) {
                    _laserHit.gameObject.SetActive(true);
                    _laser.gameObject.SetActive(true);
                    _laser.SetPosition(0, _muzzle.transform.position);
                    _laser.SetPosition(1, hit.point);
                    _laserHit.transform.position = hit.point;
                    return;
                }
                return;
            }
            _onLaserEnd?.Invoke();
        }

        public override void Exit(GameObject entity)
        {
            base.Exit(entity);
            _laser.gameObject.SetActive(false);
            _laserHit.gameObject.SetActive(false);
        }
    }
}
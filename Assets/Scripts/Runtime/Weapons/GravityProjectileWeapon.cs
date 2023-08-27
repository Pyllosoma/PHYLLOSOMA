using System;
using UnityEngine;

namespace Runtime.Weapons
{
    public class GravityProjectileWeapon : Weapon<GravityProjectileWeapon>
    {
        [SerializeField] private float _flightTime = 1f;
        [SerializeField] private float _gravity = 9.8f;
        [SerializeField] private float _shotAngle = 45f;
        [SerializeField] private Vector3 _targetPos = Vector3.zero;
        private float _currentAngle = 0f;
        private float _projectileTime = 0f;
        private bool _isFlying = false;
        private Vector3 _flightForce = Vector3.zero;
        public void Shot(Vector3 target,float shotAngle,float flightTime = 1f)
        {
            _shotAngle = shotAngle;
            _currentAngle = _shotAngle;
            _targetPos = target;
            _flightTime = flightTime;
            _isFlying = true;
            _flightForce = (_targetPos - transform.position) / _flightTime;
            _flightForce.y = _gravity * _flightTime / 2f;
            
            gameObject.SetActive(true);
        }
        private void FixedUpdate()
        {
            if (!_isFlying) return;
            _projectileTime += Time.fixedDeltaTime;
            //gravity
            _flightForce.y -= _gravity * Time.fixedDeltaTime;
            
            Vector3 position = transform.position + _flightForce;
            transform.Translate(position);
        }
        public override void Attack(GameObject target = null, Vector3 attackPoint = new Vector3())
        {
            
        }
        public override void Ready()
        {
            
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
            _isFlying = false;
            _projectileTime = 0f;
            gameObject.SetActive(false);
        }
        public override void Finish()
        {
            
        }
    }
}
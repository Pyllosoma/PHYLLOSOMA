using System;
using UnityEngine;

namespace Runtime.Weapons
{
    public class KinematicProjectile : ProjectileWeapon
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _lifeTime = 5f;
        private float _timer = 0f;
        public override void Shoot(GameObject target,Vector3 direction, float power) {
            transform.LookAt(transform.position + direction);
            _speed = power;
            _timer = 0f;
        }
        private void Update() {
            if (!gameObject.activeSelf) return;
            _timer += Time.deltaTime;
            if (_timer >= _lifeTime) {
                gameObject.SetActive(false);
                _timer = 0f;
            }
        }
        protected override void Move()
        {
            if (!gameObject.activeSelf) return;
            transform.Translate(Vector3.forward * (_speed * Time.fixedDeltaTime));
        }
    }
}
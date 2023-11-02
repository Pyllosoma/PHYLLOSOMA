using UnityEngine;

namespace Tests.Weapons
{
    public class TrackingProjectileTest : ProjectileTestWeapon
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _lifeTime = 5f;
        [SerializeField] private float _rotationSpeed = 1f;
        private float _timer = 0f;
        public override void Shoot(GameObject target,Vector3 direction, float power) {
            transform.LookAt(transform.position + direction);
            _speed = power;
            _timer = 0f;
        }
        protected override void Move()
        {
            if (!gameObject.activeSelf) return;
            TrackTarget();
            transform.Translate(Vector3.forward * (_speed * Time.fixedDeltaTime));
        }
        protected void TrackTarget()
        {
            if (!_target) return;
            var targetDirection = _target.transform.position - transform.position;
            var rotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
        }
    }
}
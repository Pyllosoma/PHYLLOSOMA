using UnityEngine;

namespace Runtime.Weapons
{
    public class RigidBodyProjectile : ProjectileWeapon
    {
        [SerializeField] private Rigidbody _rigidbody;
        public override void Shoot(GameObject target,Vector3 direction, float power) {
            _rigidbody.AddForce(direction * power, ForceMode.Impulse);
        }
        protected override void Move()
        {
            _rigidbody.MoveRotation(Quaternion.LookRotation(_rigidbody.velocity,transform.up));
        }
        

        private void OnTriggerEnter(Collider other)
        {
            if (_targetTags.Contains(other.tag)) {
                Attack(other.gameObject);
            }
            gameObject.SetActive(false);
        }
    }
}
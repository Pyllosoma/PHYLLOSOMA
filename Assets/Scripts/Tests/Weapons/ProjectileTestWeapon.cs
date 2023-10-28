using UnityEngine;

namespace Tests.Weapons
{
    public abstract class ProjectileTestWeapon : TestWeapon
    {
        private void OnEnable() => Ready();
        private void OnDisable() => Finish();
        public abstract void Shoot(GameObject target,Vector3 direction, float power);
        protected abstract void Move();
        private void FixedUpdate() => Move();
        public override bool IsUsable() => true;
    }
}
using UnityEngine;

namespace Tests.Weapons
{
    public class RangedTestWeapon : TestWeapon
    {
        [SerializeField] private Transform _muzzle;
        [SerializeField] private GameObject _projectilePrefab;
        protected override void OnAttack(GameObject target, Vector3 attackPoint)
        {
            //Debug.Log("RangedWeapon Attack!");
            var projectile = Instantiate(_projectilePrefab, _muzzle.position, Quaternion.identity);
            projectile.GetComponent<ProjectileTestWeapon>().Shoot(target,transform.forward, 10f);
        }
        public override bool IsUsable() {
            return true;
        }
    }
}
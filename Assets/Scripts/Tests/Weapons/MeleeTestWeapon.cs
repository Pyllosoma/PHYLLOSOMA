using UnityEngine;

namespace Tests.Weapons
{
    [RequireComponent(typeof(Collider))]
    public class MeleeTestWeapon : TestWeapon
    {
        protected override void OnAttack(GameObject target, Vector3 attackPoint)
        {
            Debug.Log("MeleeWeapon Attack!");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_targetTags.Contains(other.tag)) {
                Attack(other.gameObject);
            }
        }
        public override bool IsUsable()
        {
            return true;
        }
    }
}
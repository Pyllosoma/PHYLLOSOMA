using System;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Weapons
{
    [RequireComponent(typeof(Collider))]
    public class MeleeWeapon : Weapon
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
using System;
using System.Collections.Generic;
using Runtime.Attributes;
using Runtime.Utils.Targetables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Weapons
{
    [RequireComponent(typeof(Collider))]
    public class ProjectileWeapon : Weapon<ProjectileWeapon>
    {
        [TagSelector][SerializeField] protected List<string> _targetTags = new List<string>();
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rigidbody;
        private void OnEnable()
        {
            Ready();
        }
        public void Shoot(Vector3 target,float power)
        {
            _rigidbody.AddForce(target * power, ForceMode.Impulse);
            _collider.enabled = true;
        }
        public override void Ready() {
            _collider.enabled = true;
        }
        private void FixedUpdate()
        {
            if (!gameObject.activeSelf) return;
            _rigidbody.MoveRotation(Quaternion.LookRotation(_rigidbody.velocity,transform.up));
        }
        public override void Attack(GameObject target = null, Vector3 attackPoint = new Vector3())
        {
            Debug.Log("ProjectileWeapon Attack!");
        }
        private void OnTriggerEnter(Collider other)
        {
            if (_targetTags.Contains(other.tag)) {
                Attack(other.gameObject);
            }
            Debug.Log(other.tag + " : " + other.name);
            gameObject.SetActive(false);
            Debug.Log("ProjectileWeapon OnTriggerEnter!");
        }
        public override void Finish() {
            _collider.enabled = false;
        }
    }
}
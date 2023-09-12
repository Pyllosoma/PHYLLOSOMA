using System;
using System.Collections.Generic;
using Runtime.Attributes;
using Runtime.Utils.Targetables;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Runtime.Weapons
{
    public abstract class ProjectileWeapon : Weapon
    {
        private void OnEnable() => Ready();
        private void OnDisable() => Finish();
        public abstract void Shoot(Vector3 direction, float power);
        protected abstract void Move();
        private void FixedUpdate() => Move();
        public override bool IsUsable() => true;
    }
}
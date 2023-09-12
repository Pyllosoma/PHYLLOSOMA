using System;
using Runtime.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = System.Numerics.Vector3;

namespace Tests.Characters
{
    public class TestMovementComponent : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private RangedWeapon _weapon;

        private void Update()
        {
            if(Keyboard.current.spaceKey.wasPressedThisFrame) {
                _weapon.Attack();
            }
        }

        private void FixedUpdate()
        {
            if (Keyboard.current.wKey.isPressed) {
                transform.position += transform.forward * (_speed * Time.fixedDeltaTime);
            }
            if (Keyboard.current.sKey.isPressed) {
                transform.position -= transform.forward * (_speed * Time.fixedDeltaTime);
            }
            if (Keyboard.current.aKey.isPressed) {
                transform.position -= transform.right * (_speed * Time.fixedDeltaTime);
            }
            if (Keyboard.current.dKey.isPressed) {
                transform.position += transform.right * (_speed * Time.fixedDeltaTime);
            }
        }
    }
}
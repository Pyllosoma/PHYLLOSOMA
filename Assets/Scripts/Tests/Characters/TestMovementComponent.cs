using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tests.Characters
{
    public class TestMovementComponent : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
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
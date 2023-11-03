using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

namespace MovementAssets
{ 
    public class MovementAssetsInputs : MonoBehaviour
    {
        #region Vars
        //Fields
        [Header("Character Input Values")]
        [SerializeField] private Vector2 _move;
        [SerializeField] private Vector2 _look;
        [SerializeField] private bool _jump;
        [SerializeField] private bool _sprint;
        [SerializeField] private bool _kneel;

        //Properties
        public Vector2 Move { get { return _move; } }
        public Vector2 Look { get { return _look; } }
        public bool Sprint { get { return _sprint; } }
        public bool Jump { get { return _jump; } }
        public bool Kneel { get { return _kneel; } }

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;
        #endregion
        #region Input Action Methods
#if ENABLE_INPUT_SYSTEM
        private void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        private void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            { 
                LookInput(value.Get<Vector2>());
            }
        }

        private void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        private void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        private void OnKneel(InputValue value)
        {
            KneelInput(value.isPressed);
        }
#endif
        #endregion
        #region setters
        private void MoveInput(Vector2 newMoveDirection)
        {
            _move = newMoveDirection;
        }

        private void LookInput(Vector2 newLookDirection)
        {
            _look = newLookDirection;
        }

        private void JumpInput(bool newJumpState)
        {
            _jump = newJumpState;
        }

        private void SprintInput(bool newSprintState)
        {
            _sprint = newSprintState;
        }

        public void SetOffJump()
        {
            _jump = false;       
        }

        public void KneelInput(bool newKneelState)
        {
            _kneel = newKneelState;
        }

        #endregion
        #region cursor methods
        private void OnApplicationFocus(bool focus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newCursorState)
        {
            Cursor.lockState = newCursorState ? CursorLockMode.Locked : CursorLockMode.None;
        }
        #endregion
    }
}

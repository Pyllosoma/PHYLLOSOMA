using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Characters.FSM.States.Movements
{
    public class MoveState : GameObjectFSM
    {
        private enum MoveDirection
        {
            FORWARD = 0,
            BACKWARD = 1,
            LEFT = 2,
            RIGHT = 3,
            UP = 4,
            DOWN = 5
        }
        [Title("State Settings")]
        [SerializeField] private MoveDirection _moveDirection = MoveDirection.FORWARD;
        [SerializeField] private float _speed = 1f;
        public override void FixedUpdate(GameObject entity)
        {
            base.FixedUpdate(entity);
            var direction = GetDirection(entity);
            entity.transform.position += direction * (_speed * Time.fixedDeltaTime);
        }
        private Vector3 GetDirection(GameObject entity)
        {
            switch (_moveDirection)
            {
                case MoveDirection.FORWARD:
                    return entity.transform.forward;
                case MoveDirection.BACKWARD:
                    return -entity.transform.forward;
                case MoveDirection.LEFT:
                    return -entity.transform.right;
                case MoveDirection.RIGHT:
                    return entity.transform.right;
                case MoveDirection.UP:
                    return entity.transform.up;
                case MoveDirection.DOWN:
                    return -entity.transform.up;
                default:
                    return Vector3.zero;
            }
        }
    }
}
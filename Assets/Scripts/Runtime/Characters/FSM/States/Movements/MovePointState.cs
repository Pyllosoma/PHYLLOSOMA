using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Characters.FSM.States.Movements
{
    public class MovePointState : GameObjectFSM
    {
        [Title("State Settings")]
        [SerializeField] private bool _isLocal = false;
        [SerializeField] private Ease _ease = Ease.Linear;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private Vector3 _movePoint;
        [Title("Required Components")]
        [SerializeField] private Transform _target;
        [FoldoutGroup("Move Point Events")]
        [SerializeField] private UnityEvent _onMovePointReached;
        public override void Enter(GameObject target)
        {
            base.Enter(target);
            if (_isLocal) _target.DOLocalMove(_movePoint, _duration).SetEase(_ease).OnComplete(() => _onMovePointReached?.Invoke());
            else _target.DOMove(_movePoint, _duration).SetEase(_ease).OnComplete(() => _onMovePointReached?.Invoke());
        }
        
    }
}
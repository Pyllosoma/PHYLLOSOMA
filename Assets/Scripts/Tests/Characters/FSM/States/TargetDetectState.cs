using Runtime.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Tests.Characters.FSM.States
{
    public class TargetDetectState : GameObjectFSM
    {
        [Title("Required Components")]
        [InfoBox("If check target exist is true, then event will be invoked when target exist, else when target not exist")]
        [SerializeField] private bool _checkTargetExist = true;
        [SerializeField] private TargetDetector _targetDetector;
        [FoldoutGroup("Detect Events")]
        [ShowIf("_checkTargetExist")][SerializeField] private UnityEvent _onEnterRange;
        [FoldoutGroup("Detect Events")]
        [HideIf("_checkTargetExist")][SerializeField] private UnityEvent _onExitRange;
        public override void Update(GameObject entity)
        {
            switch (_targetDetector.IsTargetExist)
            {
                case true when _checkTargetExist:
                    _onEnterRange?.Invoke();
                    break;
                case false when !_checkTargetExist:
                    _onExitRange?.Invoke();
                    break;
            }
        }
    }
}
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Characters.FSM.States.Detectors
{
    public class TargetDetectState : TargetBaseState
    {
        [Title("State Settings")]
        [InfoBox("If check target exist is true, then event will be invoked when target exist, else when target not exist")]
        [SerializeField] private bool _checkTargetExist = true;
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
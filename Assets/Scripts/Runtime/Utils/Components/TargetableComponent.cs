using System;
using UnityEngine;

namespace Runtime.Utils.Components
{
    public class TargetableComponent : MonoBehaviour
    {
        public virtual bool IsTargetExist => Target;
        public virtual GameObject Target { get; set; } = null;
        public float TargetDistance => IsTargetExist ? Vector3.Distance(transform.position, Target.transform.position) : float.MaxValue;
        // public float TargetAngle{
        //     get {
        //         var result = 0f;
        //         if (_foundTargets.Count <=0) return result;
        //         var targetDirection = _foundTargets[0].transform.position - transform.position;
        //         var targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
        //         result = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetAngle);
        //         return result;
        //     }
        // }

        #if UNITY_EDITOR
        protected void Update()
        {
            if (IsTargetExist) {
                Debug.DrawLine(transform.position, Target.transform.position, Color.red);
            }
        }
        #endif
    }
}
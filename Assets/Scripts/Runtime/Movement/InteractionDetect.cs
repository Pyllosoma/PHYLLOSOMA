using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MovementAssets
{ 
    public class InteractionDetect : MonoBehaviour
    {
        #region vars
        public LayerMask pushLayers;
        public bool canPush = true;
        [Range(0.5f, 5f)] public float pushStrength = 1.1f;
        #endregion

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            //Rigidbody Push
            if (canPush)
            {
                RigidbodyPush(hit);
            }
        }

        private void RigidbodyPush(ControllerColliderHit hit)
        {
            //1. Check Rigidbody
            Rigidbody body = hit.collider.attachedRigidbody;
            if (body == null || body.isKinematic) return;

            //2. Check Layer
            var bodyLayerMask = 1 << body.gameObject.layer;
            if ((bodyLayerMask & pushLayers.value) == 0) return;

            //3. Check Bottom
            if (hit.moveDirection.y < -0.3f) return;

            //4. Calculate Push Direction
            Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z);

            //5. Push
            body.AddForce(pushDirection * pushStrength, ForceMode.Impulse);
        }
    }

}

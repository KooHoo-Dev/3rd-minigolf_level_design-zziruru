using UnityEngine;

namespace Study3D.MiniGolf
{
    public class BoostPad : MonoBehaviour
    {
        [Header("Settings")]
        public Vector3 direction = Vector3.forward;

        public float acceleration = 20f;
        
        private void OnTriggerStay(Collider other)
        {
            Rigidbody body = other.attachedRigidbody;
            if (body == null) return;
            if (body.isKinematic) return;
            
            Vector3 worldDirection = transform.TransformDirection(direction.normalized);
            body.AddForce(worldDirection * acceleration, ForceMode.Acceleration);
        }
    }
}

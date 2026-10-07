using UnityEngine;

namespace Study3D.MiniGolf
{
    public class JumpPad : MonoBehaviour
    {
        [Header("Settings")]
        public Vector3 direction = Vector3.up;
        public float power = 5f;
        
        [Tooltip("켜면 들어올 때의 수직 속도를 지워서 항상 같은 높이로 띄움")]
        public bool resetVerticalSpeed = true;
        
        private void OnTriggerEnter(Collider other)
        {
            Rigidbody body = other.attachedRigidbody;
            if (body == null || body.isKinematic) return;

            if (resetVerticalSpeed)
            {
                Vector3 velocity = body.linearVelocity;
                velocity.y = 0;
                body.linearVelocity = velocity;
            }
            
            Vector3 worldDirection = transform.TransformDirection(direction.normalized);
            body.AddForce(worldDirection * power, ForceMode.VelocityChange);
        }
    }
}

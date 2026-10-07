using UnityEngine;

namespace Study3D.MiniGolf
{
    [RequireComponent(typeof(Rigidbody))]
    public class KinematicShuttle : MonoBehaviour
    {
        [Header("Settings")]
        public Vector3 offset = new Vector3(1.6f, 0f, 0f);
        public float cycleTime = 3f;
        
        private Rigidbody body;
        private Vector3 startPosition;
        private Vector3 worldOffset;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            startPosition = transform.position;
            worldOffset = transform.TransformDirection(offset);
        }

        private void FixedUpdate()
        {
            if (cycleTime <= 0f) return;
            
            float k = Mathf.PingPong(Time.time * 2f / cycleTime, 1f);
            Vector3 target = startPosition + worldOffset * k;
            body.MovePosition(target);
        }
        
        private void OnDrawGizmosSelected()
        {
            Vector3 from = Application.isPlaying ? startPosition : transform.position;
            Vector3 to = from + transform.TransformDirection(offset);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(from, to);
            Gizmos.DrawWireSphere(from, 0.1f);
            Gizmos.DrawWireSphere(to, 0.1f);
        }
    }
}

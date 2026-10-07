using UnityEngine;

namespace Study3D.MiniGolf
{
    [RequireComponent(typeof(Rigidbody))]
    public class KinematicSpinner : MonoBehaviour
    {
        [Header("Settings")]
        public Vector3 axis = Vector3.forward;
        public float degreesPerSecond = 60f;
        
        private Rigidbody body;
        
        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (axis.sqrMagnitude < 0.0001f) return;
            float step = degreesPerSecond * Time.fixedDeltaTime;
            Quaternion delta = Quaternion.AngleAxis(step, axis.normalized);
            body.MoveRotation(body.rotation * delta);
        }
    }
}


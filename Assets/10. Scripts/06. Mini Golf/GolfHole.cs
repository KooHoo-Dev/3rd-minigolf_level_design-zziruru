using UnityEngine;

namespace Study3D.MiniGolf
{
    public class GolfHole : MonoBehaviour
    {
        private bool isComplete;

        private void OnTriggerEnter(Collider other)
        {
            if (isComplete) return;
            
            GolfBall ball = other.GetComponentInParent<GolfBall>();
            if (ball == null) return;
            isComplete = true;
            
            ball.PlaceAt(transform.position);
            if (GolfGameManager.Instance != null) GolfGameManager.Instance.CompleteHole();
            Debug.Log("홀인!");
        }
    }
}


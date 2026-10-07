using UnityEngine;

namespace Study3D.MiniGolf
{
    public class FallZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            GolfBall ball = other.GetComponentInParent<GolfBall>();
            if (ball == null) return;
            ball.ReturnToLasShotPosition();
            if (GolfGameManager.Instance != null) GolfGameManager.Instance.AddPenalty();
            Debug.Log($"[FallZone] {ball.name} 낙하 - 마지막 샷 위치로 복귀");
        }
    }
}

using UnityEngine;

namespace Study3D.MiniGolf
{
    public class TimedToggle : MonoBehaviour
    {
        [Header("대상")]
        public Behaviour[] targets;

        [Header("주기(초)")]
        public float onTime = 1.5f;
        public float offTime = 3f;

        [Tooltip("시작 시점을 이만큼 밀어서 다른 그룹과 엇갈리게 합니다.")]
        public float phaseOffset = 0f;

        private void Update()
        {
            float cycle = onTime + offTime;
            if (cycle <= 0f) return;

            // Repeat으로 0~cycle을 계속 돌면서, 앞쪽 onTime 구간이면 켭니다.
            bool isOn = Mathf.Repeat(Time.time + phaseOffset, cycle) < onTime;

            foreach (Behaviour t in targets)
                if (t != null) t.enabled = isOn;
        }
    }
}
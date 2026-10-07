using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Study.Utilities;
using TMPro;
using UnityEngine;

namespace Study3D.MiniGolf
{
    public class GolfGameManager : MonoBehaviour
    {
        public static GolfGameManager Instance { get; private set; }
        
        [Header("Settings")]
        public int par = 3;

        [Header("UI")]
        public TMP_Text label;
        public TMP_Text helpLabel; // 조작 안내용 추가
        
        [Header("Result UI")]
        public GameObject resultPanel;      // 평소엔 꺼둠
        public TMP_Text resultLabel;
        public GameObject nextHoleButton;   // 마지막 홀이면 숨김
        public GolfPlayer player;           // 완료 후 입력 차단
        
        [Header("Refs")]
        public GolfBall ball;
        public Transform teePoint;
        
        [Header("Scene Flow")]
        [Tooltip("다음 홀 씬 이름. 비우면 마지막 홀")]
        public string nextSceneName;
        
        private int strokes;          // 실제로 친 횟수
        private int penalties;        // 벌타
        private bool holeComplete;
        
        public int Total => strokes + penalties;

        private void Awake()
        {
            if(Instance == null) Instance = this;
            else { 
                Destroy(gameObject);
                return;
            }
            
            Refresh();
            if (resultPanel != null) resultPanel.SetActive(false);
        }

        private void Start()
        {
            if (ball != null && teePoint != null)
                ball.PlaceAt(teePoint.position);
            
            if (helpLabel != null)
                helpLabel.SetText(
                    "우클릭 드래그 : 시점 회전\n" +
                    "마우스 휠 : 줌\n" +
                    "스페이스 길게 → 떼기 : 샷\n" +
                    "1 : 직전 위치로 (벌타 +1)\n" +
                    "2 : 시작 위치로 (벌타 +1)\n" +
                    "R : 홀 처음부터"
                );
        }

        private void Update()
        {
            if (SimpleInput.GetKeyDown(Key.R)) Restart();
        }

        public void AddStroke()
        {
            if (holeComplete) return;

            strokes++; 
            Refresh();
        }

        public void AddPenalty()
        {
            if (holeComplete) return;
            penalties++;
            Refresh();
        }

        public void CompleteHole()
        {
            if (holeComplete) return;
            holeComplete = true;
            
            // 홀인 후 공을 또 치지 못하게 막습니다.
            if (player != null) player.enabled = false;
            
            Refresh();
            ShowResult();
        }
        
        private void ShowResult()
        {
            if (resultPanel != null) resultPanel.SetActive(true);

            if (resultLabel != null)
                resultLabel.SetText(
                    $"홀 완료!\n\n" +
                    $"{Total}타 / Par {par}  ({GetResultName()})\n" +
                    $"샷 {strokes} · 벌타 {penalties}");

            // 마지막 홀이면 '다음 홀' 버튼을 숨깁니다.
            if (nextHoleButton != null)
                nextHoleButton.SetActive(nextSceneName != "");
        }

        private void Refresh()
        {
            if (label == null) return;
            string text = $"{Total} 타 / Par {par}";
            if (holeComplete) text += $"\n{GetResultName()}";
            label.SetText(text);
        }
        
        // 기준 타수와의 차이를 골프 용어로 바꿔 줍니다.
        private string GetResultName()
        {
            // 타수 1 = 홀인원. Par와 무관하게 특별 취급합니다.
            if (Total == 1) return "홀인원!";

            int diff = Total - par;

            switch (diff)
            {
                case <= -4: return "콘도르";
                case -3:    return "앨버트로스";
                case -2:    return "이글";
                case -1:    return "버디";
                case 0:     return "파";
                case 1:     return "보기";
                case 2:     return "더블 보기";
                case 3:     return "트리플 보기";
                case 4:     return "쿼드러플 보기";
                default:    return $"+{diff}타";   // 5 이상
            }
        }
        
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void NextHole()
        {
            if (nextSceneName != "") SceneManager.LoadScene(nextSceneName);
        }
    }
}

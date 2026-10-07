using System;
using Study.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Study3D.MiniGolf
{
    public class GolfBallTester : MonoBehaviour
    {
        public GolfBall ball;
        public Vector3 startPosition;

        private void Update()
        {
            if (SimpleInput.GetKeyDown(Key.Digit1))
            {
                ball.ReturnToLasShotPosition();
                if (GolfGameManager.Instance != null)
                    GolfGameManager.Instance.AddPenalty();
            }
            if (SimpleInput.GetKeyDown(Key.Digit2))
            {
                ball.PlaceAt(startPosition);
                if (GolfGameManager.Instance != null)
                    GolfGameManager.Instance.AddPenalty();
            }
        }
    }
}
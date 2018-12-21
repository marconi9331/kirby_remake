using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirbyGame
{
    public class PoolCannonBalls : MonoBehaviour
    {
        public GameObject cannonBallPrefab;
        public Transform cannonBallContainer;
        private int currentCannonBallIndex;
        private List<BallController> cannonBalls;

        public void Awake()
        {
            cannonBalls = new List<BallController>();
            cannonBalls.Capacity = GameConstants.CANNON_BALL_NUMBER;
            AddCannonBallsToThePool();
        }

        private void AddCannonBallsToThePool()
        {
            for (int i = 0; i < GameConstants.CANNON_BALL_NUMBER; i++)
            {
                var go = Instantiate(cannonBallPrefab);
                go.SetActive(false);
                go.transform.parent = cannonBallContainer;
                cannonBalls.Add(go.GetComponent<BallController>());
            }
        }


        public BallController GetCannonBall(GameObject Shooter)
        {
            BallController b = cannonBalls[currentCannonBallIndex];
            b.ShooterGO = Shooter.GetInstanceID();
            if (b.IsActive())
            {
                print("Number of Cannon Balls not enough");
                return null;
            }

            currentCannonBallIndex = (currentCannonBallIndex + 1) % cannonBalls.Count;
            return b;
        }
    }
}
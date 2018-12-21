using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace KirbyGame
{
    public class BallController : MonoBehaviour
    {
        private Rigidbody2D ballRb;
        private Transform trans;
        private Animator ballAnimator;
        private Collider2D ballColl;
        public int ShooterGO { get; set; }


        private void Awake()
        {
            ballRb = GetComponent<Rigidbody2D>();
            trans = GetComponent<Transform>();
            ballAnimator = GetComponent<Animator>();
            ballColl = GetComponent<Collider2D>();
//            ballAnimator.speed = 0;
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(GameConstants.ENEMY_TAG) && other.isTrigger)
            {
                Physics2D.IgnoreCollision(ballColl, other);
            }
            else
            {
                ballRb.velocity = Vector2.zero;
                ballAnimator.speed = 1;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(GameConstants.GROUND_TAG))
            {
                DisableCannonBall();
            }
        }

        public void SetInMotion(Vector3 pos, Vector3 dir, GameObject shooter)
        {
            ToggleActive(true);
            Physics2D.IgnoreCollision(ballColl, shooter.GetComponent<Collider2D>());
            ballAnimator.speed = 0;
            trans.up = dir;
            trans.position = pos;
            ballRb.AddForce(trans.up * GameConstants.CANNON_BALL_FORCE);
        }

        public bool IsActive()
        {
            return gameObject.activeInHierarchy;
        }


        public void ToggleActive(bool b)
        {
            gameObject.SetActive(b);
        }

        public void DisableCannonBall()
        {
            gameObject.SetActive(false);
        }
    }
}
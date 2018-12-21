using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.Networking.NetworkSystem;
using Debug = UnityEngine.Debug;

namespace KirbyGame
{
    public class CannonController : MonoBehaviour
    {
        private Transform cannonTrans;
        public GameObject muzzle;
        private Transform muzzleTrans;
        public Transform targetTrans;
        private Quaternion rotation;
        private Vector3 relativePos;
        private float relativeDist;
        private Animator cannonAnim;
        private BoxCollider2D cannonColl;
        private float timer;

        public PoolCannonBalls cannonBallPool;

        void Start()
        {
            cannonTrans = GetComponent<Transform>();
            muzzleTrans = muzzle.transform;
            cannonAnim = GetComponent<Animator>();
            cannonColl = gameObject.GetComponent<BoxCollider2D>();
            cannonAnim.speed = 0f;
        }

        void Update()
        {
            relativePos = cannonTrans.position - targetTrans.position;
            relativeDist = relativePos.magnitude;
            rotation = Quaternion.LookRotation(Vector3.back, relativePos);
            cannonAnim.SetFloat("Angle", rotation.eulerAngles.z);
            timer += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if (Physics2D.Raycast(cannonTrans.position, transform.TransformDirection(targetTrans.position - cannonTrans.position), 10f, 1 << 8))
            {
                if (timer >= GameConstants.CANNON_BALL_RELOAD)
                {
                    timer = 0;
                    var dir = muzzleTrans.position - cannonTrans.position;
                    var cannonBall = cannonBallPool.GetCannonBall(gameObject);
                    cannonBall.SetInMotion(muzzleTrans.position, dir, gameObject);
                }
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
//            if (other.CompareTag(GameConstants.PLAYER_TAG) && timer >= GameConstants.CANNON_BALL_RELOAD)
//            {
//                timer = 0;
//                var dir = muzzleTrans.position - cannonTrans.position;
//                var cannonBall = cannonBallPool.GetCannonBall(gameObject);
//                cannonBall.SetInMotion(muzzleTrans.position, dir, gameObject);
//            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.IsTouching(cannonColl) && other.CompareTag(GameConstants.ENEMY_BULLET_TAG) &&
                other.gameObject.GetComponent<BallController>().ShooterGO != gameObject.GetInstanceID())
            {
                cannonAnim.speed = 1f;
                cannonAnim.SetBool("Death", true);
            }
        }

        public void DisableCannon()
        {
            gameObject.SetActive(false);
        }

//        private void OnDrawGizmos()
//        {
//            Gizmos.color = Color.yellow;
//            Gizmos.DrawWireSphere(cannonTrans.position, 10);
//        }
    }
}
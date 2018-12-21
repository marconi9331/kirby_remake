using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace KirbyGame
{
    public class FlyingSaucerController : MonoBehaviour
    {
        private Rigidbody2D SaucerRB;
        private Vector2 SaucerVelocity;
        public Transform[] waypoints;
        protected Vector3[] WorldWaypoints;
        private int CurrentPoint;
        private int NextPoint;

        private void Start()
        {
            SaucerRB = GetComponent<Rigidbody2D>();

            WorldWaypoints = new Vector3[waypoints.Length];
            for (int i = 0; i < WorldWaypoints.Length; i++)
            {
                WorldWaypoints[i] = transform.TransformPoint(waypoints[i].position - SaucerRB.transform.position);
            }

            for (int i = 0; i < waypoints.Length; i++)
            {
                Destroy(waypoints[i].gameObject);
            }

//            SaucerRB.transform.position = Vector3.zero;
            SaucerRB.transform.position = WorldWaypoints[0];

        }

        private void FixedUpdate()
        {
            float distanceToGo = GameConstants.FLYING_SAUCER_SPEED * Time.deltaTime;

            while (distanceToGo > 0)
            {
                Vector2 direction = WorldWaypoints[NextPoint] - transform.position;

                float dist = distanceToGo;

                if (direction.sqrMagnitude < dist * dist)
                {
                    dist = direction.magnitude;

//                    CurrentPoint = NextPoint;

                    NextPoint += 1;
                    if (NextPoint >= WorldWaypoints.Length)
                    {
                        NextPoint = 0;
                    }
                }

                SaucerVelocity = direction.normalized * dist;
                SaucerRB.MovePosition(SaucerRB.position + SaucerVelocity);

                distanceToGo -= dist;
            }
        }

       
    }
}
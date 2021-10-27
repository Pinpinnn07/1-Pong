using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trajectory : MonoBehaviour
{
    public BallControl ball;
    CircleCollider2D ballcollider;
    Rigidbody2D ballrb;

    public GameObject ballAtCollision;
   
    // Start is called before the first frame update
    void Start()
    {
        ballrb = ball.GetComponent<Rigidbody2D>();
        ballcollider = ball.GetComponent<CircleCollider2D>();       
    }

    // Update is called once per frame
    void Update()
    {
        bool drawBallAtCollision = false;

        Vector2 offsetHitPoint = new Vector2();

        RaycastHit2D[] circleCastHit2dArray = Physics2D.CircleCastAll(ballrb.position, ballcollider.radius, ballrb.velocity.normalized);

        foreach (RaycastHit2D circleCastHit2D in circleCastHit2dArray)
        {
            Vector2 hitpoint = circleCastHit2D.point;
            Vector2 hitNormal = circleCastHit2D.normal;
            offsetHitPoint = hitpoint + hitNormal * ballcollider.radius;

            if (circleCastHit2D.collider != null && circleCastHit2D.collider.GetComponent<BallControl>() == null)
            {
                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);
            }

            if (circleCastHit2D.collider.GetComponent<SideWall>() == null)
            {
                Vector2 inVector = (offsetHitPoint - ball.TrajectoryOrigin).normalized;
                Vector2 outVector = Vector2.Reflect(inVector, hitNormal);

                float outDot = Vector2.Dot(outVector, hitNormal);
                if (outDot > -1.0f && outDot < 1.0)
                {
                    DottedLine.DottedLine.Instance.DrawDottedLine(offsetHitPoint, offsetHitPoint + outVector * 5.0f);
                    drawBallAtCollision = true;
                }
                if (drawBallAtCollision)
                {
                    ballAtCollision.transform.position = offsetHitPoint;
                    ballAtCollision.SetActive(true);
                }
                else
                {
                    ballAtCollision.SetActive(false);
                }
            }           
        }       
    }

 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A paddle for pong game
/// </summary>
public class Paddle : MonoBehaviour
{
    [SerializeField]
    ScreenSide side;
    private float fixedUpdateCount = 0;
    private Rigidbody2D rb2d;
    float halfColliderHeight;
    float BounceAngleHalfRange = Mathf.PI / 3;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
	{
        rb2d = GetComponent<Rigidbody2D>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        halfColliderHeight = collider.size.y / 2;

    }
	
	/// <summary>
    /// Updates per second
    /// </summary>
    void FixedUpdate()
    {
        fixedUpdateCount += 1;

        CalculateClampedY();
        //Left Paddle Movement
        if (Input.GetAxis("LeftPaddle") != 0 && side == ScreenSide.Left)
        {
            Vector2 position = transform.position;
            position.y += Input.GetAxis("LeftPaddle") * ConfigurationUtils.PaddleMoveUnitsPerSecond * Time.fixedDeltaTime;
            transform.position = position;
        }

        //Right Paddle Movement
        if (Input.GetAxis("RightPaddle") != 0 && side == ScreenSide.Right)
        {
            Vector2 position = transform.position;
            position.y += Input.GetAxis("RightPaddle") * ConfigurationUtils.PaddleMoveUnitsPerSecond * Time.fixedDeltaTime;
            transform.position = position;
        }
        
    }

    /// <summary>
    /// Calculates the clamp
    /// </summary>
   
    void CalculateClampedY()
    {
        Vector2 position = transform.position;
        if(position.y + halfColliderHeight > ScreenUtils.ScreenTop)
        {
            position.y = ScreenUtils.ScreenTop - halfColliderHeight;
        }

        else if(position.y - halfColliderHeight < ScreenUtils.ScreenBottom)
        {
            position.y = ScreenUtils.ScreenBottom + halfColliderHeight;
        }

        transform.position = position;
    }

    /// <summary>
    /// Detects collision with a ball to aim the ball
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            // calculate new ball direction
            float ballOffsetFromPaddleCenter =
                coll.transform.position.y - transform.position.y;
            float normalizedBallOffset = ballOffsetFromPaddleCenter /
                halfColliderHeight;
            float angleOffset = normalizedBallOffset * BounceAngleHalfRange;

            // angle modification is based on screen side
            float angle;
            if (side == ScreenSide.Left)
            {
                angle = angleOffset;
            }
            else
            {
                angle = (float)(Mathf.PI - angleOffset);
            }
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // tell ball to set direction to new direction
            Ball ballScript = coll.gameObject.GetComponent<Ball>();
            ballScript.SetDirection(direction);
        }
    }

}

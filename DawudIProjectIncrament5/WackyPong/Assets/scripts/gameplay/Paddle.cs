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

    //Scoring Support
    HUD hud;

    //Freeze support
    bool frozen = false;
    Timer freezeTimer;
    


    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
	{
        rb2d = GetComponent<Rigidbody2D>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        halfColliderHeight = collider.size.y / 2;

        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();

        //freeze support
        freezeTimer = gameObject.AddComponent<Timer>();
    }
	
    void Update()
    {
        if (freezeTimer.Finished)
        {
            Unfreeze(); //Defrost
        }
    }

	/// <summary>
    /// Updates per second
    /// </summary>
    void FixedUpdate()
    {
        if (!frozen)
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
        
        //print("Here?");
        if (coll.gameObject.CompareTag("Ball") && fixingCollision(coll))
        {
            //print("here now?");
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

            //Add number of hits
            hud.AddHits(side, ballScript.Hits);

        }
        

        //Prints out the points of collision for the function fixingCollision
        /*if (fixingCollision(coll))
        {
            print(coll.GetContact(0).point.x);
        }*/
       
    }

    /// <summary>
    /// Fixes the collision 
    /// </summary>
    /// <param name="coll"></param>
    /// <returns></returns>
    bool fixingCollision(Collision2D coll)
    {

        ContactPoint2D[] results;
        results = new ContactPoint2D[5];
        coll.GetContacts(results);
        //print(results[0].point.x);
        
        if (Mathf.Abs(results[0].point.x) > 5.7 && Mathf.Abs(results[0].point.x) < 5.8)
        {
            return true;
        }

        else
        {
            return false;
        }
      
    }

    /// <summary>
    /// Method to freeze our paddle for a certain amount of time
    /// </summary>
    /// <param name="freezeSide"></param>
    /// <param name="duration"></param>
    public void Freeze(ScreenSide freezeSide, float duration)
    {
        if (freezeSide == side)
        {
            //Freeze the paddle and start the timer or add time
            frozen = true;
            if (!freezeTimer.Running)
            {
                freezeTimer.Duration = duration;
                freezeTimer.Run();
            }

            else
            {
                freezeTimer.AddTime(duration);
            }
        }
    }

    /// <summary>
    /// Unfreezes our paddle
    /// </summary>
    void Unfreeze()
    {
        frozen = false;
        freezeTimer.Stop();
    }
}

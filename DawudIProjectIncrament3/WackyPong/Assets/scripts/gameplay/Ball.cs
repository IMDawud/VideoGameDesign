using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ball
/// </summary>
public class Ball : MonoBehaviour
{
    //Variable for our timer
    Timer timer;
    Timer spawnTimer;
    
    //Variables in general
    Rigidbody2D rb2d;
    Vector2 flyer;

    //Variable for paddle hit count
    float hits;

    //Variable for points
    float points;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        float choose = Random.Range(0, 2);
        float angle1 = Random.Range((3 * Mathf.PI) / 4, (5 * Mathf.PI) / 4); //135 degrees and 225 degrees to radians going to the left
        float angle2 = Random.Range(-(Mathf.PI) / 4, Mathf.PI / 4); //-45 degrees and 45 degrees to radians going to the right


        if (choose < 1)
        {
            Vector2 leftDirection = new Vector2(Mathf.Cos(angle1), Mathf.Sin(angle1));
            flyer = leftDirection * ConfigurationUtils.BallImpulseForce;
        }
        else
        {
            Vector2 rightDirection = new Vector2(Mathf.Cos(angle2), Mathf.Sin(angle2));
            flyer = rightDirection * ConfigurationUtils.BallImpulseForce;
        }

        //Timer components
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = ConfigurationUtils.BallLifetime;
        timer.Run();

        //Delayed spawn timer
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = 2;
        spawnTimer.Run();

        //HUD components for hits and points
        hits = ConfigurationUtils.Hits;
        points = ConfigurationUtils.Points;
    }

    /// <summary>
    /// Update is called once per frame (might need to change this to fixed update)
    /// </summary>
    void Update()
    {
        if (timer.Finished)
        {
            //Destroy(gameObject);
            SpawnAndDestroy();    
        }

        //Timer delay
        if (spawnTimer.Stop())
        {
            rb2d.AddForce(flyer, ForceMode2D.Impulse);
        }
    }



    /// <summary>
    /// Set Direction for curve paddles
    /// </summary>
    /// <param name="x"></param>
    public void SetDirection(Vector2 x)
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = ConfigurationUtils.BallImpulseForce * x;
    }

    /// <summary>
    /// Destroys ball if it leaves playing field (Use clamps to keep in playing field)
    /// </summary>
    void OnBecameInvisible()
    {
        Vector2 position = transform.position;
        if (!timer.Finished && position.x < ScreenUtils.ScreenLeft)
        {
            
            //For the points screen left
            GameObject.FindGameObjectWithTag ("HUD").GetComponent<HUD>().AddScore(ScreenSide.Right, points);
            SpawnAndDestroy();
        }
        else if (!timer.Finished && position.x > ScreenUtils.ScreenRight)
        {

            //For the points screen right
            GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>().AddScore(ScreenSide.Left, points); 
            SpawnAndDestroy();
        }
    }

    /// <summary>
    /// Destroys the ball. Is referenced in Update and OnBecameInvisible
    /// </summary>
    public void SpawnAndDestroy()
    {
        Destroy(gameObject);
        Camera.main.GetComponent<BallSpawner>().ballSpawn();
    
    }

    /// <summary>
    /// Returns the number of hits
    /// </summary>
    public float Hits
    {
        get { return hits; }
    }

    /// <summary>
    /// Returns the points
    /// </summary>
    public float Points
    {
        get { return points; }
    }
}

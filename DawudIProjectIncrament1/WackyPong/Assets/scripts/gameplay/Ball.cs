using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ball
/// </summary>
public class Ball : MonoBehaviour
{


    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();

        float choose = Random.Range(0, 2);
        float angle1 = Random.Range((3 * Mathf.PI) / 4, (5 * Mathf.PI) / 4); //135 degrees and 225 degrees to radians going to the left
        float angle2 = Random.Range(-(Mathf.PI) / 4, Mathf.PI / 4); //-45 degrees and 45 degrees to radians going to the right



        if (choose < 1)
        {
            Vector2 leftDirection = new Vector2(Mathf.Cos(angle1), Mathf.Sin(angle1));
            rb2d.AddForce(leftDirection * ConfigurationUtils.BallImpulseForce, ForceMode2D.Impulse);
        }
        else
        {
            Vector2 rightDirection = new Vector2(Mathf.Cos(angle2), Mathf.Sin(angle2));
            rb2d.AddForce(rightDirection * ConfigurationUtils.BallImpulseForce, ForceMode2D.Impulse);
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
    /// Destroys ball if it leaves playing field
    /// </summary>
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}

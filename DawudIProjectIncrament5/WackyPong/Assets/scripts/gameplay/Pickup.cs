using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Ball
{
    float duration;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        if (ballType == BallType.Freezer)
        {
            duration = ConfigurationUtils.FreezerSeconds;
        }
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (ballType == BallType.Freezer)
        {
            if (coll.gameObject.CompareTag("LeftPaddle"))
            {
                GameObject paddle = GameObject.FindGameObjectWithTag("RightPaddle");
                Paddle paddleScript = paddle.GetComponent<Paddle>();
                paddleScript.Freeze(ScreenSide.Right, duration);
            }

            else if (coll.gameObject.CompareTag("RightPaddle"))
            {
                GameObject paddle = GameObject.FindGameObjectWithTag("LeftPaddle");
                Paddle paddleScript = paddle.GetComponent<Paddle>();
                paddleScript.Freeze(ScreenSide.Left, duration);
            }
        }

        Camera.main.GetComponent<BallSpawner>().ballSpawn();
        Destroy(gameObject);
    }
}

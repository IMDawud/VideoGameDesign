using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    //For the score
    [SerializeField]
    Text scoreText;
    int scoreLeft;
    int scoreRight;
    const string ScorePrefix = " - ";

    //For the left hits
    [SerializeField]
    Text leftHits;
    int lefth;
    const string LeftHitPrefix = "Hits: ";

    //For the right hits
    [SerializeField]
    Text rightHits;
    int righth;
    const string RightHitPrefix = "Hits: ";


    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = scoreLeft.ToString() + ScorePrefix + scoreRight.ToString();

        leftHits.text = LeftHitPrefix + lefth.ToString();
        rightHits.text = RightHitPrefix + righth.ToString();

        //Add the listener
        EventManager.AddListener(AddScore);
        EventManager.AddListener(AddHits);
    }

    /// <summary>
    /// To calculate the hits for each players paddle
    /// </summary>
    /// <param name="side"></param>
    /// <param name="numHits"></param>
    public void AddHits(ScreenSide side, int numHits)
    {
        print(numHits);
        if (side == ScreenSide.Left)
        {
            
            lefth += numHits;
            leftHits.text = LeftHitPrefix + lefth.ToString();
        }

        else if (side == ScreenSide.Right)
        {
            righth += numHits;
            rightHits.text = RightHitPrefix + righth.ToString();
        }
    }

    /// <summary>
    /// To calculate the score
    /// </summary>
    /// <param name="scoringSide"></param>
    /// <param name="amountAdded"></param>
    void AddScore(ScreenSide scoredSide, int amountAdded)
    {
        if(scoredSide == ScreenSide.Left)
        {
            scoreLeft += amountAdded;
        }

        else if(scoredSide == ScreenSide.Right)
        {
            scoreRight += amountAdded;
        }

        scoreText.text = scoreLeft.ToString() + ScorePrefix + scoreRight.ToString();

    }

}

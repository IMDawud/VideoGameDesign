using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    //For the score
    [SerializeField]
    Text scoreText;
    float scoreLeft;
    float scoreRight;
    const string ScorePrefix = " - ";

    //For the left hits
    [SerializeField]
    Text leftHits;
    float lefth;
    const string LeftHitPrefix = "Hits: ";

    //For the right hits
    [SerializeField]
    Text rightHits;
    float righth;
    const string RightHitPrefix = "Hits: ";


    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = scoreLeft.ToString() + ScorePrefix + scoreRight.ToString();

        leftHits.text = LeftHitPrefix + lefth.ToString();
        rightHits.text = RightHitPrefix + righth.ToString();
    }

    /// <summary>
    /// To calculate the hits for each players paddle
    /// </summary>
    /// <param name="side"></param>
    /// <param name="numHits"></param>
    public void AddHits(ScreenSide side, float numHits)
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
    public void AddScore(ScreenSide scoredSide, float amountAdded)
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

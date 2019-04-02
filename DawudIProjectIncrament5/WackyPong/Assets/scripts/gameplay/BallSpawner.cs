using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ball spawner
/// </summary>
public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject baller; //For the standard ball

    [SerializeField]
    GameObject prefabBonusBall; //For the bonus ball

    [SerializeField]
    GameObject prefabFreezerPickup; //For the freeze ball


    // Spawn Controls
    Timer spawnTimer;

    //Collision Free Info
    const int MaxNumSpawnTry = 10;

    float ballColliderRadius1; //Don't need both but did it anyways
    float ballColliderRadius2;

    Vector2 min = new Vector2();
    Vector2 max = new Vector2();


	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
        //Start the timer for spawning balls
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = Random.Range(ConfigurationUtils.MinSpawnDelay, ConfigurationUtils.MaxSpawnDelay);
        spawnTimer.Run();

        //Retrieve collider values
      
        GameObject temporaryBall = Instantiate(baller) as GameObject;
        CircleCollider2D collider = temporaryBall.GetComponent<CircleCollider2D>();
        
        ballColliderRadius1 = collider.radius / 2;
        ballColliderRadius2 = collider.radius / 2;
        //print(ballColliderHalfHeight);
        //print(ballColliderHalfWidth);
        Destroy(temporaryBall);
 
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
		if (spawnTimer.Finished)
        {
            ballSpawn();

            spawnTime();
        }
	}


    /// <summary>
    /// Our Ball Spawner updated
    /// </summary>
    public void ballSpawn()
    {

        bool pendingOverlap = true;
        int spawnRepeat = 1;
        float randomType = Random.value;
        while (Physics2D.OverlapArea(min, max) == null && spawnRepeat < MaxNumSpawnTry && pendingOverlap == true)
        {
            //GameObject newBall = Instantiate(baller) as GameObject;
            //pendingOverlap = false;
            spawnRepeat++;
            if (randomType < ConfigurationUtils.StandardSpawnProbability)
            {
                GameObject newBall = Instantiate(baller) as GameObject;
                pendingOverlap = false;
            }

            else if (randomType < ConfigurationUtils.StandardSpawnProbability + ConfigurationUtils.BonusSpawnProbability)
            {
                GameObject bonusBall = Instantiate(prefabBonusBall) as GameObject;
                pendingOverlap = false;
            }

            else
            {
                GameObject freezerPickup = Instantiate(prefabFreezerPickup) as GameObject;
                pendingOverlap = false;
            }
        }

    }

    /// <summary>
    /// This is meant to change the timer within the update when the previous timer finished
    /// </summary>
    public void spawnTime()
    {
            //Change spawn timer duration and restart
            spawnTimer.Duration = Random.Range(ConfigurationUtils.MinSpawnDelay, ConfigurationUtils.MaxSpawnDelay);
            spawnTimer.Run();
    }

    /// <summary>
    /// Use fo the minimum and maximum values within the ball spawn
    /// </summary>
    /// <param name="location"></param>
    void SetMinAndMax(Vector3 location)
    {
        min.x = location.x - ballColliderRadius1;
        min.y = location.y - ballColliderRadius2;
        max.x = location.x + ballColliderRadius1;
        max.y = location.y + ballColliderRadius2;
    }

}

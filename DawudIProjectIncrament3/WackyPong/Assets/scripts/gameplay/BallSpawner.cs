using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ball spawner
/// </summary>
public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject baller;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
		
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
		
	}

    public void ballSpawn()
    {
        GameObject newBall = Instantiate(baller) as GameObject;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides access to configuration data
/// </summary>
public static class ConfigurationUtils
{
    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    public static float PaddleMoveUnitsPerSecond
    {
        get { return 10; }
    }

    /// <summary>
    /// Gets the ball moving
    /// </summary>
    public static float BallImpulseForce
    {
        get { return 5; }
    }

    /// <summary>
    /// Life time of ball
    /// </summary>
    public static float BallLifetime
    {
        get { return 20; }
    }

    /// <summary>
    /// Number of hits to add
    /// </summary>
    public static float Hits
    {
        get { return 1; }
    }

    /// <summary>
    /// Number of points to add
    /// </summary>
    public static float Points
    {
        get { return 1; }
    }

    #endregion

    /// <summary>
    /// Initializes the configuration utils
    /// </summary>
    public static void Initialize()
    {

    }
}

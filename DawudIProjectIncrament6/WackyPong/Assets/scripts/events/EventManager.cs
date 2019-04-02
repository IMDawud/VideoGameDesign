using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    /// <summary>
    /// For points
    /// </summary>
    static List<Ball> invokers = new List<Ball>();
    static List<UnityAction<ScreenSide, int>> listeners = new List<UnityAction<ScreenSide, int>>();


    public static void AddInvoker(Ball invoker)
    {
        invokers.Add(invoker);
        foreach(UnityAction<ScreenSide, int> listener in listeners)
        {
            invoker.AddPointsAddedEventListener(listener);
        }
    }

    public static void AddListener(UnityAction<ScreenSide, int> handler)
    {
        listeners.Add(handler);
        foreach(Ball ball in invokers)
        {
            ball.AddPointsAddedEventListener(handler);
        }
    }

    /// <summary>
    /// For Hits
    /// </summary>
    static List<Paddle> invokersH = new List<Paddle>();
    static List<UnityAction<ScreenSide, int>> listenersH = new List<UnityAction<ScreenSide, int>>();

    public static void AddInvokerH(Paddle invoker)
    {
        invokersH.Add(invoker);
        foreach(UnityAction<ScreenSide, int> listener in listenersH)
        {
            invoker.AddHitsAddedEventListener(listener);
        }
    }

    public static void AddListenerH(UnityAction<ScreenSide, int> handler)
    {
        listenersH.Add(handler);
        foreach (Paddle paddle in invokersH)
        {
            paddle.AddHitsAddedEventListener(handler);
        }
    }

}

using System;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public static bool isDay { get; set; }
    public static bool isFpv { get; set; }
    public static int room {  get; set; }
    public static float minFpvDistance { get; set; } = 0.9f;
    public static float maxFpvDistance { get; set; } = 2.0f;


    public static float effectsVolume { get; set; }
    public static float ambientVolume { get; set; }
    public static float musicVolume { get; set; }

    public static List<String> collectedItems { get; set; } = new();
    #region Game events
    private const string broadcastkey = "Broadcast";
    // Emit Signal Trigger Dispatch [Event]
    public static void TriggerGameEvent(String eventName, object data)
    {
        if (subscribers.ContainsKey(eventName))
        {
            foreach (var action in subscribers[eventName])
            {
                action(eventName, data);
            }
        }
    }

    private static Dictionary<string, List<Action<String, object>>> subscribers = new();
    public static void Subscribe(Action<String, object> action, String eventName)
    {
        if (subscribers.ContainsKey(eventName))
        {
            subscribers[eventName].Add(action);
        }
        else
        {
            subscribers[eventName] = new() { action };
        }
    }
    public static void Unsubscribe(Action<String, object> action, String eventName)
    {
        if (subscribers.ContainsKey(eventName))
        {
            subscribers[eventName].Remove(action);
        }
        else Debug.LogWarning("Unsubscribe of not subscribed key: " + eventName);
    }
    #endregion
}
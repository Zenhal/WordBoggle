using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHandler
{
    private static EventHandler instance;

    public UnityEvent<string> OnWordSubmitted;

    public EventHandler()
    {
        InitialiseEvents();
    }

    public static EventHandler Instance
    {
        get
        {
            if (instance == null)
                instance = new EventHandler();
            return instance;
        }
    }

    private void InitialiseEvents()
    {
        OnWordSubmitted = new UnityEvent<string>();
    }
}

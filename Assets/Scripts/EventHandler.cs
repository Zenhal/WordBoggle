using System.Collections.Generic;
using UnityEngine.Events;

public class EventHandler
{
    private static EventHandler instance;

    public UnityEvent<string, List<Tile>> OnWordSubmitted;
    public UnityEvent<LevelObjective> OnObjectiveInitialised;
    public UnityEvent<int, int> OnObjectiveUpdated;
    public UnityEvent<string, bool> OnWordProcessed;
    public UnityEvent<bool> OnLevelEnd;
    public UnityEvent RestartGame;
    public UnityEvent NextLevel;
    public UnityEvent<string> UpdateTimer;
    public UnityEvent DisableTimer;

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
        OnWordSubmitted = new UnityEvent<string, List<Tile>>();
        OnObjectiveInitialised = new UnityEvent<LevelObjective>();
        OnObjectiveUpdated = new  UnityEvent<int, int>();
        OnWordProcessed = new UnityEvent<string, bool>();
        OnLevelEnd = new UnityEvent<bool>();
        RestartGame = new UnityEvent();
        NextLevel = new UnityEvent();
        UpdateTimer = new UnityEvent<string>();
        DisableTimer = new UnityEvent();
    }
}

using System;

public class GameTimer
{
    public event Action OnTimerStarted;
    public event Action OnTimerPaused;
    public event Action OnTimerResumed;
    public event Action OnTimerEnded;
    public event Action OnTimerReset;
    public event Action<float> OnTimerTick; 

    private float duration;
    private float remainingTime;
    private bool isRunning;
    
    public GameTimer(float duration)
    {
        this.duration = duration;
        remainingTime = duration;
    }

    public void Start()
    {
        isRunning = true;
        OnTimerStarted?.Invoke();
    }

    public void Pause()
    {
        isRunning = false;
        OnTimerPaused?.Invoke();
    }

    public void Resume()
    {
        isRunning = true;
        OnTimerResumed?.Invoke();
    }

    public void Reset()
    {
        remainingTime = duration;
        isRunning = false;
        OnTimerReset?.Invoke();
    }

    public void Restart()
    {
        Reset();
        Start();
    }

    public void Update(float deltaTime)
    {
        if (!isRunning) return;
        
        remainingTime -= deltaTime;
        OnTimerTick?.Invoke(remainingTime);
        
        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            isRunning = false;
            OnTimerEnded?.Invoke();
        }
    }

    
    public string GetFormattedTime()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(remainingTime);
        return $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
    }
}
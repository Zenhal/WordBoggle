using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelMode : GameMode
{
    private LevelObjective currentObjective;
    private GameTimer gameTimer;
    
    public override void InitializeGame(LevelData levelData)
    {
        SetLevelObjective(levelData);
        CheckAndStartTimer();
    }

    private void SetLevelObjective(LevelData levelData)
    {
        currentObjective = new LevelObjective();

        if (levelData.totalScore > 0)
        {
            SetScoreObjective(levelData);
        }
        else
        {
            SetWordObjective(levelData);
        }
        EventHandler.Instance.OnObjectiveInitialised.Invoke(currentObjective);
    }

    private void SetScoreObjective(LevelData levelData)
    {
        currentObjective.type = LevelObjectiveType.Score;
        currentObjective.value = levelData.totalScore;
        currentObjective.timeValue = levelData.timeSec;
    }

    private void SetWordObjective(LevelData levelData)
    {
        currentObjective.type = LevelObjectiveType.Word;
        currentObjective.value = levelData.wordCount;
        currentObjective.timeValue = levelData.timeSec;
    }

    private void CheckAndStartTimer()
    {
        if (currentObjective.timeValue == 0) return;
        gameTimer = new GameTimer(currentObjective.timeValue);
        gameTimer.Start();
        gameTimer.OnTimerEnded += OnTimerEnded;
    }

    public override void Update()
    {
        if (gameTimer != null)
        {
            gameTimer?.Update(Time.deltaTime);
            EventHandler.Instance.UpdateTimer.Invoke(gameTimer?.GetFormattedTime());
        }
    }

    public override void ProcessValidWord(List<Tile> wordTiles, string word)
    {
        RegisterWord(word);
        AddScore(CalculateScore(word));
        CheckObjective();
    }

    private void CheckObjective()
    {
        EventHandler.Instance.OnObjectiveUpdated.Invoke(currentScore, wordsAdded.Count);
        
        switch (currentObjective.type)
        {
            case LevelObjectiveType.Score:
            {
                if (IsLevelCompleted(currentScore, currentObjective.value))
                {
                    EndGame(true);
                }
            }
                break;
            case LevelObjectiveType.Word:
            {
                if (IsLevelCompleted(wordsAdded.Count, currentObjective.value))
                {
                    EndGame(true);
                }
                
            }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private bool IsLevelCompleted(int count, int value) => count >= value;

    public override void EndGame(bool isWin)
    {
        if(isWin)
            Debug.Log("Objective Reached");
        
        EventHandler.Instance.OnLevelEnd.Invoke(isWin);
        gameTimer.Reset();
    }

    private void OnTimerEnded()
    {
        EndGame(false);
        gameTimer.OnTimerEnded -= OnTimerEnded;
        gameTimer = null;
    }
    
    
    
    
}
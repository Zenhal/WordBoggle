using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI wordCountText;
    [SerializeField] private TextMeshProUGUI wordText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject victoryPopup;
    [SerializeField] private GameObject failedPopup;

    private LevelObjective levelObjective;

    private string objectiveScore;
    private string objectiveWordCount;
    private void Start()
    {
        AddListeners();
    }

    private void AddListeners()
    {
        EventHandler.Instance.OnWordProcessed.AddListener(UpdateWordText);
        EventHandler.Instance.OnObjectiveInitialised.AddListener(OnObjectiveInitialised);
        EventHandler.Instance.OnObjectiveUpdated.AddListener(OnObjectiveUpdated);
        EventHandler.Instance.OnLevelEnd.AddListener(OnLevelEnd);
        EventHandler.Instance.UpdateTimer.AddListener(UpdateTimer);
        EventHandler.Instance.DisableTimer.AddListener(DisableTimer);
    }

    private void RemoveListeners()
    {
        EventHandler.Instance.OnWordProcessed.RemoveListener(UpdateWordText);
        EventHandler.Instance.OnObjectiveInitialised.RemoveListener(OnObjectiveInitialised);
        EventHandler.Instance.OnObjectiveUpdated.RemoveListener(OnObjectiveUpdated);
        EventHandler.Instance.OnLevelEnd.RemoveListener(OnLevelEnd);
        EventHandler.Instance.UpdateTimer.RemoveListener(UpdateTimer);
        EventHandler.Instance.DisableTimer.RemoveListener(DisableTimer);
    }

    private void OnObjectiveInitialised(LevelObjective objective)
    {
        Reset();
        levelObjective = objective;
        switch (levelObjective.type)
        {
            case LevelObjectiveType.Score:
            {
                objectiveScore = "/" + levelObjective.value;
            }
                break;
            case LevelObjectiveType.Word:
            {
                objectiveWordCount = "/" + levelObjective.value;
            }
                break;
            default:
                break;
        }
        
    }

    private void Reset()
    {
        objectiveScore = string.Empty;
        objectiveWordCount  = string.Empty;
        UpdateScore(0);
        UpdateWordCount(0);
    }

    private void OnObjectiveUpdated(int score, int wordsAdded)
    {
        UpdateScore(score);
        UpdateWordCount(wordsAdded);
    }

    private void UpdateScore(int score)
    {
        scoreText.text = score + objectiveScore;
    }

    private void UpdateWordCount(int wordCount)
    {
        wordCountText.text = wordCount + objectiveWordCount;
    }

    private void UpdateWordText(string word, bool isValid)
    {
        if(!isValid)
            wordText.color = Color.red;
        
        wordText.text = word;
        Invoke(nameof(ResetWordText), 1f);
    }

    private void ResetWordText()
    {
        wordText.color = Color.white;
        wordText.text = string.Empty;
    }

    private void OnLevelEnd(bool isWin)
    {
        if (isWin)
        {
            victoryPopup.SetActive(true);
        }
        else
        {
            failedPopup.SetActive(true);
        }
    }

    private void UpdateTimer(string timer)
    {
        timerText.text = timer;
    }

    private void DisableTimer()
    {
        timerText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }
}

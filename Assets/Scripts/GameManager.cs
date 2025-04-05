using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private BoardGenerator boardGenerator;
    
    private InputManager m_inputManager;
    private WordValidator m_wordValidator;
    
    private HashSet<string> wordsAdded = new HashSet<string>();

    private void Start()
    {
        Init();
        AddListeners();
    }

    private void AddListeners()
    {
        EventHandler.Instance.OnWordSubmitted.AddListener(OnWordSubmitted);
    }

    private void RemoveListeners()
    {
        EventHandler.Instance.OnWordSubmitted.RemoveListener(OnWordSubmitted);
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void Init()
    {
         boardGenerator.Init();
         m_inputManager = new InputManager(inputHandler);
         m_wordValidator = new WordValidator();
    }

    private void OnWordSubmitted(string word)
    {
        if (IsValidWord(word))
        {
            if (IsWordAdded(word))
            {
                Debug.Log("Word already added");
                return;
            }

            Debug.Log("Word Valid : " + word);
            wordsAdded.Add(word);
            return;
        }

        Debug.Log("Word Invalid : " + word);
    }

    private bool IsValidWord(string word) => m_wordValidator.IsValidWord(word);
    private bool IsWordAdded(string word) => wordsAdded.Contains(word);
}

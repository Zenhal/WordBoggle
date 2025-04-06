using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private BoardGenerator boardGenerator;
    
    private InputManager m_inputManager;
    private WordValidator m_wordValidator;
    private GameMode currentGameMode;


    private int currentLevel = 0;
    private HashSet<string> wordsAdded = new HashSet<string>();

    private void Start()
    {
        Init();
        AddListeners();
    }

    private void AddListeners()
    {
        EventHandler.Instance.OnWordSubmitted.AddListener(OnWordSubmitted);
        EventHandler.Instance.RestartGame.AddListener(RestartGame);
        EventHandler.Instance.NextLevel.AddListener(NextLevel);
    }

    private void RemoveListeners()
    {
        EventHandler.Instance.OnWordSubmitted.RemoveListener(OnWordSubmitted);
        EventHandler.Instance.RestartGame.RemoveListener(RestartGame);
        EventHandler.Instance.NextLevel.RemoveListener(NextLevel);
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void Init()
    {
         wordsAdded.Clear();
         m_inputManager ??= new InputManager(inputHandler);
         m_wordValidator ??= new WordValidator();
         LoadGame();
    }

    private void LoadGame()
    {
        currentGameMode = MainManager.Instance.GetGameMode();
        
        if(currentGameMode is LevelMode)
            LoadLevelMode();
        else
        {
            LoadEndlessMode();
        }
        
    }

    private void Update()
    {
        currentGameMode?.Update();
    }

    private void LoadLevelMode()
    {
        currentGameMode = new LevelMode();
        GetCurrentLevel();
        var levelDataManager = new LevelDataManager();
        var levelData = levelDataManager.GetLevelDataList();
        boardGenerator.Init(levelData.data[currentLevel]);
        currentGameMode.InitializeGame(levelData.data[currentLevel]);
    }

    private void LoadEndlessMode()
    {
        currentGameMode = new EndlessMode(boardGenerator);
        boardGenerator.InitialiseRandomBoard(4,4 );
        currentGameMode.InitializeGame(null);
    }

    private void OnWordSubmitted(string word, List<Tile> wordTiles)
    {
        var isValid = IsValidWord(word);
        if (isValid)
        {
            if (IsWordAdded(word))
            {
                Debug.Log("Word already added");
                return;
            }

            Debug.Log("Word Valid : " + word);
            wordsAdded.Add(word);
            currentGameMode.ProcessValidWord(wordTiles, word);
            EventHandler.Instance.OnWordProcessed.Invoke(word, true);
            return;
        }
        
        EventHandler.Instance.OnWordProcessed.Invoke(word, false);
        Debug.Log("Word Invalid : " + word);
    }

    private void GetCurrentLevel()
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel", 0);
    }

    private void UpdateLevelNumber()
    {
        currentLevel++;
        PlayerPrefs.SetInt("currentLevel", currentLevel);
    }

    private void RestartGame()
    {
        boardGenerator.ClearBoard();
        Init();
    }

    private void NextLevel()
    {
        boardGenerator.ClearBoard();
        UpdateLevelNumber();
        Init();
    }

    private bool IsValidWord(string word) => m_wordValidator.IsValidWord(word);
    private bool IsWordAdded(string word) => wordsAdded.Contains(word);
}

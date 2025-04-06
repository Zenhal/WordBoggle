using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    
    private GameMode gameMode;
    private void Start()
    {
        if (Instance == null)
            Instance = this;
        
        DontDestroyOnLoad(this);
    }

    public void OnLevelModeClicked()
    {
        gameMode = new LevelMode();
        GoToGameScene();
    }

    public void OnEndlessModeClicked()
    {
        gameMode = new EndlessMode(null);
        GoToGameScene();
    }

    private void GoToGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public GameMode GetGameMode() => gameMode;
}

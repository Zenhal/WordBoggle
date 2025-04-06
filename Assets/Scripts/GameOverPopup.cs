using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPopup : MonoBehaviour
{
    public void OnRestartButtonClicked()
    {
        EventHandler.Instance.RestartGame.Invoke();
        gameObject.SetActive(false);
    }

    public void OnQuitButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void OnNextLevelButtonClicked()
    {
        EventHandler.Instance.NextLevel.Invoke();
        gameObject.SetActive(false);
    }
}

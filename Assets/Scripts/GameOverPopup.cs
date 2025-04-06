using UnityEngine;

public class GameOverPopup : MonoBehaviour
{
    public void OnRestartButtonClicked()
    {
        EventHandler.Instance.RestartGame.Invoke();
        gameObject.SetActive(false);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void OnNextLevelButtonClicked()
    {
        EventHandler.Instance.NextLevel.Invoke();
        gameObject.SetActive(false);
    }
}

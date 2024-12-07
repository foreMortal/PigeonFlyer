using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using YG;

public class GameEndedMenu : MonoBehaviour
{
    [SerializeField] private GameObject StopMenu;
    private bool windowState;
    public UnityEvent<bool> gameStateChanged = new();
    
    private void Awake()
    {
        YandexGame.onVisibilityWindowGame += ChangeState;
    }

    private void ChangeState(bool state)
    {
        if (!state)
        {
            windowState = YandexGame.isGamePlaying;
            YandexGame.GameplayStop();
        }
        else
        {
            if (windowState)
                YandexGame.GameplayStart();
            else
                YandexGame.GameplayStop();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void StopGame()
    {
        YandexGame.GameplayStop();
        StopMenu.SetActive(true);
        Time.timeScale = 0f;
        gameStateChanged.Invoke(false);
    }

    public void Resume()
    {
        YandexGame.GameplayStart();
        StopMenu.SetActive(false);
        Time.timeScale = 1f;
        gameStateChanged.Invoke(true);
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}

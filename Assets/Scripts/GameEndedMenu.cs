using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameEndedMenu : MonoBehaviour
{
    [SerializeField] private GameObject StopMenu;

    public UnityEvent<bool> gameStateChanged = new();

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void StopGame()
    {
        StopMenu.SetActive(true);
        Time.timeScale = 0f;
        gameStateChanged.Invoke(false);
    }

    public void Resume()
    {
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

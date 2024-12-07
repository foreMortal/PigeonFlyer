using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class StartGameCanvas : MonoBehaviour, IInputTaker
{
    [SerializeField] private GameObject InputHandler;
    [SerializeField] private CoinsScriptableObject coinsObj;
    [SerializeField] private GameObject Hud;
    [SerializeField] private Text distanceTillBoss;

    private IInputManager inputManager;

    private void OnEnable()
    {
        inputManager.ManageInputEvent(this, true);
        Time.timeScale = 0f;
    }

    private void Awake()
    {
        inputManager = InputHandler.GetComponent<IInputManager>();
        if(YandexGame.lang == "en")
            distanceTillBoss.text = "Till Boss: " + coinsObj.distanceTillBoss.ToString("F0") + "<color=red>m</color>";
        else if(YandexGame.lang == "ru")
            distanceTillBoss.text = "До Босса: " + coinsObj.distanceTillBoss.ToString("F0") + "<color=red>m</color>";
        Hud.SetActive(false);

        transform.GetChild(0).gameObject.SetActive(true);
    }

    public Action<bool> GetInputAction()
    {
        return StartGame;
    }

    private void StartGame(bool state)
    {
        Hud.SetActive(true);
        inputManager.ManageInputEvent(this, false);
        YandexGame.GameplayStart();
        Time.timeScale = 1;

        Destroy(gameObject);
    }
}

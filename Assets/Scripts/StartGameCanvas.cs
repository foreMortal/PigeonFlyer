using System;
using UnityEngine;
using UnityEngine.UI;

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
        distanceTillBoss.text = "Till Boss: " + coinsObj.distanceTillBoss.ToString("F0") + "<color=red>m</color>";
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
        Destroy(gameObject);
        Time.timeScale = 1;
    }
}

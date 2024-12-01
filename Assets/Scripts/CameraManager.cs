using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject endCanvas, ToolCanvas, Hud;
    [SerializeField] private CameraLateUpdateRoutine[] routineScripts;

    private bool active;
    private float gameEndTimer;

    public event Action playerDied;

    private void OnEnable()
    {
        PigeonHealth.PigeonDied += PlayerDied;
    }
    private void OnDisable()
    {
        PigeonHealth.PigeonDied -= PlayerDied;
    }

    private void Update()
    {
        if(gameEndTimer > 0f)
        {
            gameEndTimer -= Time.deltaTime;
            if(gameEndTimer <= 0f)
            {
                endCanvas.SetActive(true);
                ToolCanvas.SetActive(false);
                Hud.SetActive(false);
                //Time.timeScale = 0f;
            }
        }
    }

    private void PlayerDied()
    {
        playerDied?.Invoke();
        gameEndTimer = 1f;
    }

    private void Awake()
    {
        active = true;
    }

    private void LateUpdate()
    {
        if (active)
        {
            foreach (var r in routineScripts)
            {
                r.PerformRoutine();
            }
        }
    }
}

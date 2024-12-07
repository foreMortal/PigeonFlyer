using System;
using UnityEngine;
using YG;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private EggCrack crack;
    [SerializeField] private GameObject ToolCanvas, Hud;
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
                ToolCanvas.SetActive(false);
                Hud.SetActive(false);
                crack.OpenEndMenu();
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

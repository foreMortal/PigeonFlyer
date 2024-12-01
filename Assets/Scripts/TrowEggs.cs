using System;
using UnityEngine;

[RequireComponent(typeof(EggsManager))]
public class TrowEggs : MonoBehaviour, IInputTaker
{
    [SerializeField] private float TimeBetweenShots;
    [SerializeField] GameObject inputHandler;

    private EggsManager manager;
    private IInputManager inputManager;
    private bool active;
    private float timer;

    private void Awake()
    {
        active = true;
        manager = GetComponent<EggsManager>();
        inputManager = inputHandler.GetComponent<IInputManager>();
    }

    private void OnEnable()
    {
        inputManager.ManageInputEvent(this, true);
        PigeonHealth.PigeonDied += StopGame;
    }
    private void OnDisable()
    {
        inputManager.ManageInputEvent(this, false);
        PigeonHealth.PigeonDied -= StopGame;
    }

    private void StopGame()
    {
        active = false;
    }

    private void FireEgg(bool state)
    {
        if (active)
        {
            if (state && timer <= 0f)
            {
                manager.ThrowEgg(Vector3.right);
                timer = TimeBetweenShots;
            }
        }
    }

    private void Update()
    {
        if (active)
        {
            if (timer > 0f) timer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireEgg(true);
        }
    }

    public void OnGameStateChenged(bool state)
    {
        active = state;
        manager.ChangeGameState(active);
    }
    public Action<bool> GetInputAction()
    {
        return FireEgg;
    }
}

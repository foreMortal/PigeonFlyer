using System;
using UnityEngine;

[RequireComponent(typeof(EggsManager))]
public class TrowEggs : MonoBehaviour, IInputTaker
{
    [SerializeField] protected float TimeBetweenShots;

    protected EggsManager manager;
    private IInputManager inputManager;
    protected bool active;
    protected float timer;

    private void Awake()
    {
        active = true;
        manager = GetComponent<EggsManager>();
    }

    public void SetInputManager(IInputManager m)
    {
        inputManager = m;
        inputManager.ManageInputEvent(this, true);
    }

    private void OnEnable()
    {
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

    protected virtual void FireEgg(bool state)
    {
        if (active)
        {
            if (state && timer <= 0f)
            {
                manager.ThrowEgg(transform.position, Vector3.right);
                timer = TimeBetweenShots;
            }
        }
    }

    protected virtual void Update()
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

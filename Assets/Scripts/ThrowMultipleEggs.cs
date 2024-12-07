using UnityEngine;

public class ThrowMultipleEggs : TrowEggs
{
    [SerializeField] private Vector3[] directions;
    [SerializeField] private bool delay;
    [SerializeField] private float delayTime;

    private bool inThrow;
    private int delayedIdx = 99;
    private float timerDelay;

    protected override void Update()
    {
        base.Update();

        if(Time.time >= timerDelay && delayedIdx < directions.Length)
        {
            DelayedThrow();

            if (delayedIdx >= directions.Length)
            {
                timer = TimeBetweenShots;
                inThrow = false;
            }
        }
    }

    protected override void FireEgg(bool state)
    {
        if (active)
        {
            if (!delay && state && timer <= 0f)
                NoDelayThrow();
            else if (state && !inThrow && timer <= 0f)
            {
                delayedIdx = 0;
                DelayedThrow();
                inThrow = true;
            }
        }
    }

    private void NoDelayThrow()
    {
        for (int i = 0; i < directions.Length; i++)
        {
            manager.ThrowEgg(transform.position, directions[i]);
        }
        timer = TimeBetweenShots;
    }

    private void DelayedThrow()
    {
        manager.ThrowEgg(transform.position, directions[delayedIdx++]);
        timerDelay = Time.time + delayTime;
    }
}

using System;
using UnityEngine;
using DG.Tweening;

public class FalconHealth : MonoBehaviour, ICanGetHitted
{
    [SerializeField] private float maxHealth;
    [SerializeField] private FalconManager manager;

    private Animator animator;
    private float health, t;
    private bool changePos, active = true;
    private Vector3 deathPos;

    public static Action FalconDied;

    public void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Arrive()
    {
        health = maxHealth;
        active = true;
    }

    private void LateUpdate()
    {
        if (changePos)
        {
            if(t < 1)
            {
                t += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(deathPos, Vector3.zero, t);
            }
            else
            {
                transform.localPosition = Vector3.zero;
                changePos = false;
                animator.Play("Leave");
            }
        }
    }

    public void GetHitted(float damage)
    {
        if (active)
        {
            health -= damage;

            if (health <= 0f)
            {
                active = false;
                changePos = true;
                deathPos = transform.localPosition;
                animator.Play("FlyFast");
                FalconDied?.Invoke();
            }
        }
    }

    public void Die()
    {
        manager.FalconDied();
    }
}

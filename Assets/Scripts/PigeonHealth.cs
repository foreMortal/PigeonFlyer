using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PigeonHealth : MonoBehaviour
{
    [SerializeField] private float deathPunchX, deathPunchY;
    [SerializeField] private float deathGravity;
    [SerializeField] private LayerMask mask;

    private ContactFilter2D filter;
    private Collider2D[] results = new Collider2D[1];
    private Rigidbody2D rb;
    private Animator animator;
    private bool firstInvoke = true;

    public static event Action PigeonDied;

    private void Awake()
    {
        filter.SetLayerMask(mask);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(Time.frameCount%2 == 1)
        {
            if(Physics2D.OverlapCircle(transform.position + new Vector3(0f, -0.06f), 0.3f, filter, results) > 0)
            {
                PigeonFly fly = GetComponent<PigeonFly>();
                Destroy(fly);

                animator.SetBool("Flying", false);

                int dir = results[0].transform.position.x > transform.position.x ? -1 : 1;

                rb.freezeRotation = false;
                rb.gravityScale = 1f;
                rb.angularVelocity = Random.Range(150f, 360f);
                rb.velocity = new Vector2(deathPunchX * dir, deathPunchY);

                if (firstInvoke)
                {
                    PigeonDied?.Invoke();
                    firstInvoke = false;
                }
            }
        }
    }
}

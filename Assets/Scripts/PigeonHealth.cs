using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PigeonHealth : MonoBehaviour
{
#if UNITY_EDITOR
    public float x, y, rad;
#endif

    [SerializeField] private float deathPunchX, deathPunchY;
    [SerializeField] private float deathGravity;
    [SerializeField] private LayerMask mask;

    private ContactFilter2D filter;
    private Collider2D[] results = new Collider2D[1];
    private Rigidbody2D rb;
    private Animator animator;
    private bool firstInvoke = true, end;
    private float endTimer;

    public static event Action PigeonDied;

    private void Awake()
    {
        filter.SetLayerMask(mask);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + new Vector3(x, y), rad);
    }
#endif

    private void Update()
    {
        if (end && endTimer < Time.time)
        {
            end = false;
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if(Time.frameCount%2 == 1)
        {
            if(Physics2D.OverlapCircle(transform.position + new Vector3(-0.35f, -0.06f), 0.25f, filter, results) > 0)
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
                    end = true;
                    endTimer = Time.time + 5f;
                }
            }
        }
    }
}

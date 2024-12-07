using System;
using UnityEngine;

public class PigeonFly : MonoBehaviour, IInputTaker
{
    [SerializeField] private float speed, gravityScale, flyingUpStrength, maxVertSpeed, minVertSpeed;

    private ContactFilter2D filter;
    private Vector3 direction, gravity;
    private Vector3[] offset = new Vector3[2] { new Vector3(0f, 0.17f, 0f), new Vector3(0f, -0.3f, 0f)};
    private Collider2D[] results = new Collider2D[1];
    private Rigidbody2D rb;
    private Animator animator;
    private IInputManager inputManager;
    private bool active, inputHeldDown;

    public float Speed { get { return speed; } set { speed = value; } }

    private void OnDisable()
    {
        inputManager.ManageInputEvent(this, false);
    }

    public void SetInputManager(IInputManager m)
    {
        inputManager = m;
        inputManager.ManageInputEvent(this, true);
    }

    private void Awake()
    {
        active = true;
        filter.SetLayerMask(1 << 7);

        animator = GetComponent<Animator>();
        direction.x = speed;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (active)
        {
            direction.x = speed;

            if (inputHeldDown) gravity.y += flyingUpStrength * Time.deltaTime;

            gravity.y -= Time.deltaTime * gravityScale;

            if (gravity.y > maxVertSpeed)
                gravity.y = maxVertSpeed;
            else if (gravity.y < minVertSpeed)
                gravity.y = minVertSpeed;

            animator.SetBool("Flying", inputHeldDown);
        }
    }

    public void GetPlayerInput(bool held)
    {
        inputHeldDown = held;

        if (inputHeldDown) gravity.y += flyingUpStrength * Time.deltaTime;
    }

    public Action<bool> GetInputAction()
    {
        return GetPlayerInput;
    }

    private void FixedUpdate()
    {
        if (active)
        {
            int idx = Time.frameCount % 2;

            if (Physics2D.OverlapBox(transform.position + offset[idx], new Vector3(0.5f, 0.1f, 0f), 0f, filter, results) > 0)
            {
                if (idx == 0 && gravity.y > 0)
                    gravity.y = 0;
                else if (idx == 1 && gravity.y < 0)
                    gravity.y = 0;
            }

            rb.velocity = direction + gravity;
        }
    }

    public void OnGameStateChenged(bool state)
    {
        active = state;
    }
}

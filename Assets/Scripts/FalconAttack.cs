using UnityEngine;

public class FalconAttack : MonoBehaviour
{
    [SerializeField] private GameObject dashRadius, round;
    [SerializeField] private Vector3[] radiusPositions;
    [SerializeField] private float timeBetweenAttacks;

    private EggsManager eggManager;
    private Animator animator;
    private Falcon movement;

    private string attackAnimName;
    private bool performAttack = true, canAttack;
    private float timeDelay;
    private float attackTimer;
    private int eggIndex;
    private Vector3[] throwVecs = new Vector3[15];

    //private event Action attack, attackComplete;

    private void Awake()
    {
        eggManager = GetComponentInParent<EggsManager>();
        animator = GetComponent<Animator>();
        movement = GetComponentInParent<Falcon>();
    }

    private void OnEnable()
    {
        PigeonHealth.PigeonDied += Stop;
        FalconHealth.FalconDied += Stop;

        attackTimer = timeBetweenAttacks;
        canAttack = true;
    }

    private void OnDisable()
    {
        PigeonHealth.PigeonDied -= Stop;
        FalconHealth.FalconDied -= Stop;
    }

    private void Stop() 
    {
        dashRadius.SetActive(false);
        enabled = false;
    }

    private void Update()
    {
        if (canAttack)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f)
            {
                int rng = Random.Range(0, 6);
                switch (rng)
                {
                    case 0://Dash
                        SetupAttack(0.75f, 4f, radiusPositions[0], "FalcconDash", true);
                        break;
                    case 1://Upper
                        SetupAttack(1f, 2f, radiusPositions[1], "AttackFromUp", true);
                        break;
                    case 2://Lower
                        SetupAttack(1f, 2f, radiusPositions[2], "AttckFromDown", true);
                        break;
                    case 3:
                        SetupThrowAttack(0, "ThrowAttack");
                        break;
                    case 4:
                        SetupThrowAttack(1, "ThrowUp");
                        break;
                    case 5:
                        SetupThrowAttack(2, "ThrowDown");
                        break;
                }
            }
        }
        else
        {
            if (performAttack && Time.time >= timeDelay)
            {
                performAttack = false;

                AttackMoment();

                //attack?.Invoke();
            }
        }
    }

    /*private void OnDrawGizmos()
    {
        float x = -0.4f, y = 0.6f, delta = 0.2f;
        for (int i = 6; i > -1; i--)
        {
            throwVecs[i] = new Vector3(x, y);
            if (i > 3) x -= delta;
            else x -= -delta;
            y -= delta;

            vecs[i] = throwVecs[i];
        }
        foreach (var t in vecs)
        {
            Gizmos.DrawLine(transform.position, transform.position + t);
        }
    }*/

    private void SetupThrowAttack(int index, string name)
    {
        eggIndex = 0;

        if(index == 1 || index == 2)
        {
            for(int i = 0; i < 15; i++)
            {
                throwVecs[i] = -Vector3.right;
            }

            if(index == 1)
                SetupAttack(1f, 2f, radiusPositions[3], name, true);
            if(index == 2)
                SetupAttack(1f, 2f, radiusPositions[4], name, true);
        }
        else
        {
            float x = -0.4f, y = 0.6f, delta = 0.2f;
            for (int i = 6; i > -1; i--)
            {
                throwVecs[i] = new Vector3(x, y);
                if (i > 3) x -= delta;
                else x -= -delta;
                y -= delta;
            }
            round.SetActive(true);
            SetupAttack(1f, 0.5f, Vector3.zero, name, false);
        }
    }

    public void RoundAttack()
    {
        round.SetActive(false);
        for(int i = 0; i < 7; i++)
        {
            eggManager.ThrowEgg(throwVecs[eggIndex++]);
        }
    }

    public void FireEgg()
    {
        eggManager.ThrowEgg(throwVecs[eggIndex++]);
    }

    private void SetupAttack(float verSpeed, float delay, Vector3 radiusPos, string animName, bool radius)
    {
        dashRadius.SetActive(radius);

        if(radius)
            dashRadius.transform.SetLocalPositionAndRotation(new Vector3(radiusPos.x, radiusPos.y), Quaternion.Euler(0f, 0f, radiusPos.z));

        attackAnimName = animName;
        movement.VerSpeed = verSpeed;
        timeDelay = Time.time + delay;

        performAttack = true;
        canAttack = false;
    }

    private void AttackMoment()
    {
        movement.VerSpeed = 0f;
        animator.Play(attackAnimName, 0);
    }

    public void AttackComplete()
    {
        movement.RestoreSpeed();
        dashRadius.SetActive(false);
        canAttack = true;
        attackTimer = timeBetweenAttacks;

        //attackComplete?.Invoke();
    }
}

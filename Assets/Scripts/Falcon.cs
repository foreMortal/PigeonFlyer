using UnityEngine;
using DG.Tweening;

public class Falcon : MonoBehaviour
{
    [SerializeField] private PigeonSpawner spawner;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float ySpeed;

    private Transform pigeon;
    private Vector3 startOffset = new Vector3(15f, 5f, 0);
    private bool active, pigeonGavered;
    private float arriveTimer, verSpeed, yPos;

    public float VerSpeed { get { return verSpeed; } set { verSpeed = value; } }

    private void OnEnable()
    {
        PigeonHealth.PigeonDied += StopGame;
    }
    private void OnDisable()
    {
        PigeonHealth.PigeonDied -= StopGame;
    }

    void StopGame()
    {
        enabled = false;

        Tweener a = DOTween.To(() => transform.position, x => transform.position = x, transform.position - new Vector3(25f, 0f, 0f), 2f);
        a.onComplete += () => { gameObject.SetActive(false); };
    }

    public void Arrive()
    {
        if (!pigeonGavered)
        {
            pigeon = spawner.PlayerTransform;
            pigeonGavered = true;
        }
        gameObject.SetActive(true);
        transform.position = pigeon.position + startOffset;
        arriveTimer = 0f;
        verSpeed = ySpeed;
        active = false;
    }

    private void Update()
    {
        if (active)
        {
            Moving();
        }
        else
        {
            arriveTimer += Time.deltaTime * 0.5f;

            transform.position = Vector3.Lerp(pigeon.transform.position + startOffset, pigeon.transform.position + offset, arriveTimer);
            if(arriveTimer >= 1f)
            {
                active = true;
                yPos  = transform.position.y;
            }
        }
    }

    private void Moving()
    {
        float x = pigeon.position.x + offset.x;

        yPos += (pigeon.position.y - transform.position.y) * verSpeed * Time.deltaTime;

        transform.position = new Vector3(x, yPos);
    }

    public void RestoreSpeed()
    {
        verSpeed = ySpeed;
    }
}

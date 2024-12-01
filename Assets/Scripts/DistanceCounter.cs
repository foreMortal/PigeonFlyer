using UnityEngine;
using UnityEngine.UI;

public class DistanceCounter : MonoBehaviour
{
    [SerializeField] private Text distanceText, best;
    [SerializeField] private CoinsScriptableObject coinObj;

    private PigeonFly fly;
    private float prevPosX;
    private float distancePassed, lastDelta;
    private int prevTextValue;
    private bool active;

    public float Distance { get { return distancePassed; } }
    public float LastDelta { get { return lastDelta; } }

    private void OnDisable()
    {
        PigeonHealth.PigeonDied -= Stop;
    }
    private void OnEnable()
    {
        PigeonHealth.PigeonDied += Stop;
    }

    private void Awake()
    {
        fly = GetComponent<PigeonFly>();
        best.text = "Best: " + coinObj.maxDistance + "m";
        distanceText.text = prevTextValue + " <color=red>m</color>";
        active = true;
    }

    private void Stop()
    {
        active = false;
    }

    void Update()
    {
        if (active)
        {
            lastDelta = transform.position.x - prevPosX;

            fly.Speed += lastDelta * 0.005f;

            distancePassed += lastDelta;
            prevPosX = transform.position.x;

            if ((int)distancePassed > prevTextValue)
            {
                prevTextValue = (int)distancePassed;
                distanceText.text = prevTextValue + " <color=red>m</color>";
            }
        }
    }
}

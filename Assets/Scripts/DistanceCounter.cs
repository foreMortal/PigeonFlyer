using UnityEngine;
using UnityEngine.UI;
using YG;

public class DistanceCounter : MonoBehaviour
{
    [SerializeField] private Text distanceText, best;
    [SerializeField] private CoinsScriptableObject coinObj;

    private PigeonFly fly;
    private float prevPosX;
    private float distancePassed, lastDelta;
    private int prevTextValue;
    private bool active;
    private string record;

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

    public void SetTexts(Text distance, Text best)
    {
        if (YandexGame.lang == "ru")
            record = "Рекорд: ";
        else if (YandexGame.lang == "en")
            record = "Best: ";

        distanceText = distance;
        this.best = best;

        fly = GetComponent<PigeonFly>();

        best.text = record + coinObj.maxDistance + "m";
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

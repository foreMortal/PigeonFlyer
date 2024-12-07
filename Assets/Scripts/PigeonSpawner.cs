using System;
using UnityEngine;
using UnityEngine.UI;

public class PigeonSpawner : MonoBehaviour
{
    [SerializeField] private Text dist, best, coins;
    [SerializeField] private EggCrack crack;
    [SerializeField] private Transform InputCanvas, ToolCanvas, coinsImage;
    [SerializeField] private CoinsScriptableObject coinsObj;
    [SerializeField] private GameObject[] Pigeons;

    private PigeonFly fly;
    private DistanceCounter distanceCounter;
    private CoinsCollector coinsCollector;
    private PigeonHealth pgHealth;

    public CoinsCollector Collector { get { return coinsCollector; } }
    public DistanceCounter Counter { get { return distanceCounter; } }
    public Transform PlayerTransform { get { return fly.transform; } }

    private void Awake()
    {
        GameObject Pigeon = Instantiate(Pigeons[coinsObj.pigeonIdx], new Vector3(-4f, 0f), Quaternion.identity);
        GameEndedMenu gameEnd = ToolCanvas.GetComponent<GameEndedMenu>();

        fly = Pigeon.GetComponent<PigeonFly>();
        distanceCounter = Pigeon.GetComponent<DistanceCounter>();
        pgHealth = Pigeon.GetComponent<PigeonHealth>();
        coinsCollector = Pigeon.GetComponent<CoinsCollector>();
        TrowEggs throwEggs = Pigeon.GetComponent<TrowEggs>();

        fly.SetInputManager(InputCanvas.GetChild(0).GetComponent<IInputManager>());
        throwEggs.SetInputManager(InputCanvas.GetChild(1).GetComponent<IInputManager>());

        distanceCounter.SetTexts(dist, best);
        coinsCollector.SelfAwake(coins, coinsImage);
        crack.SelfAwake(coinsCollector, distanceCounter);

        gameEnd.gameStateChanged.AddListener(fly.OnGameStateChenged);
        gameEnd.gameStateChanged.AddListener(throwEggs.OnGameStateChenged);
    }
}

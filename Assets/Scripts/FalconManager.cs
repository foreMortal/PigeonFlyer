using System.Collections.Generic;
using UnityEngine;

public class FalconManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rewardCoin;
    [SerializeField] private PigeonSpawner collectorHandler;
    [SerializeField] private Falcon falcon;
    [SerializeField] private FalconHealth health;
    [SerializeField] private CoinsScriptableObject coinsObj;
    [SerializeField] private DistanceCounter playerCounter;

    private CoinsCollector collector;
    private List<Rigidbody2D> coins = new List<Rigidbody2D>();
    private bool active, arrived;
    private int rewardForBoss;
    private CameraManager manager;

    private void OnEnable()
    {
        manager.playerDied += PlayerDied;
    }

    private void OnDisable()
    {
        manager.playerDied -= PlayerDied;
    }

    private void Start()
    {
        collector = collectorHandler.Collector;
        playerCounter = collectorHandler.Counter;
    }

    private void Awake()
    {
        rewardForBoss = 25 + (coinsObj.levelNumber * 25);
        manager = GetComponent<CameraManager>();
        active = true;
    }

    private void PlayerDied()
    {
        active = false;
    }

    private void Update()
    {
        if (Time.frameCount % 10 != 0)
            return;

        if(active && coinsObj.distanceTillBoss <= 0f)
        {
            falcon.Arrive();
            health.Arrive();

            active = false;
            arrived = true;
            
            coinsObj.distanceTillBoss = 150f + (25f * coinsObj.defeatedBosses);
        }
    }

    private void LateUpdate()
    {
        if(!arrived)
            coinsObj.distanceTillBoss -= playerCounter.LastDelta;
    }

    public void FalconDied()
    {
        arrived = false;
        active = true;
        falcon.gameObject.SetActive(false);

        for(int i = 0; i < 10; i++)
        { 
            Rigidbody2D c = Instantiate(rewardCoin);
            c.transform.SetPositionAndRotation(transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 3.75f, +10f), Quaternion.identity);
            c.velocity = new Vector3(2.5f, 1f);
            coins.Add(c);
            collector.CollectReward(coins, rewardForBoss);
        }

        coinsObj.defeatedBosses++;
        coinsObj.distanceTillBoss = 150f + (25f * coinsObj.defeatedBosses);
    }
}

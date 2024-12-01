using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCollector : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform coin;

    private Collider2D[] results = new Collider2D[1];
    private ContactFilter2D filter;
    private int coins, rewardForBoss;
    private bool active, kinematic;
    private float collectTime = 2f;
    private List<Rigidbody2D> rewardedCoins;
    private List<Vector3> rewardedPos = new List<Vector3>();

    public int Coins { get { return coins; } set { coins = value; } }

    private void OnDisable()
    {
        PigeonHealth.PigeonDied -= Stop;
    }
    private void OnEnable()
    {
        PigeonHealth.PigeonDied += Stop;
    }

    private void Stop()
    {
        active = false;
    }

    private void Awake()
    {
        filter.SetLayerMask(1 << 8);
        filter.useTriggers = true;

        active = true;
        UpdateCoinsUI();
    }

    private void FixedUpdate()
    {
        if (active)
        {
            if (Time.frameCount % 2 == 0)
            {
                if (Physics2D.OverlapCircle(transform.position + new Vector3(0f, -0.06f), 0.3f, filter, results) > 0)
                {
                    results[0].GetComponent<ICollectables>().Collect(this);
                }
            }
        }
    }

    private void Update()
    {
        if(collectTime < 1f)
        {
            collectTime += Time.deltaTime;
            if(collectTime > 0f)
            {
                if (!kinematic)
                {
                    for(int i = 0; i < rewardedCoins.Count; i++)
                    {
                        rewardedCoins[i].isKinematic = true;
                        if (i < rewardedPos.Count)
                            rewardedPos[i] = rewardedCoins[i].position;
                        else
                            rewardedPos.Add(rewardedCoins[i].position);
                    }

                    kinematic = true;
                }
                for (int i = 0; i < rewardedCoins.Count; i++)
                {
                    rewardedCoins[i].transform.position = Vector3.Lerp(rewardedPos[i], coin.position, collectTime);
                }
            }
            if(collectTime >= 1f)
            {
                for(int i = 0; i < rewardedCoins.Count; i++)
                {
                    rewardedCoins[i].gameObject.SetActive(false);
                }
                coins += rewardForBoss;
                UpdateCoinsUI();
            }
        }
    }

    public void UpdateCoinsUI()
    {
        coinsText.text = coins.ToString("F0");
    }

    public void CollectReward(List<Rigidbody2D> list, int rewardForBoss)
    {
        kinematic = false;
        collectTime = -1f;
        rewardedCoins = list;
        rewardedPos = new List<Vector3>();
        this.rewardForBoss = rewardForBoss;
    }
}

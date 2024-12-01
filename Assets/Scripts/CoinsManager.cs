using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private Coin coinPrefab;
    private List<Coin> hidedCoins = new List<Coin>();

    private float timer;

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
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            timer = 1f;
            if(hidedCoins.Count > 0)
            {
                hidedCoins[0].gameObject.SetActive(true);
                hidedCoins[0].Initialize(new Vector3(transform.position.x + 10f, 0f, 0f));
                hidedCoins.RemoveAt(0);
            }
            else
            {
                Coin c = Instantiate(coinPrefab);
                c.SelfAwake(this);
                c.Initialize(new Vector3(transform.position.x + 10f, 0f, 0f));
            }
        }
    }

    public void SendForReuse(Coin c)
    {
        c.gameObject.SetActive(false);
        hidedCoins.Add(c);
    }
}

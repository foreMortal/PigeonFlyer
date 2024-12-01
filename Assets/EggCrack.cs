using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Threading.Tasks;

public class EggCrack : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private CoinsScriptableObject coinsObj;
    [SerializeField] private FalconManager falcon;
    [SerializeField] private CoinsCollector coinsCollector;
    [SerializeField] private DistanceCounter distanceCounter;
    [SerializeField] private GameObject endMenu;
    [SerializeField] private Text coinsText, distanceText;
    [SerializeField] private Transform coinsTarget;
    [SerializeField] private Vector2 min, max, rotRange;
    [SerializeField] private float tapsNeeded;
    [SerializeField] private GameObject coin;

    private Transform[] coins = new Transform[15];
    private Animator animator;
    private int taps, nextStep, steps = 1;
    private int dir = 1;
    private float timer, coinsValue = 0, coinsGavered, distance= 0, distanceTarget;
    private bool active, ended;
    private Tweener tweener;
    private DataLoaderService dataLoader;

    private void Awake()
    {
        dataLoader = new();
        animator = GetComponent<Animator>();
        coinsGavered = coinsCollector.Coins;
        distanceTarget = distanceCounter.Distance;

        nextStep = (int)(tapsNeeded * 0.3f);
        CountStatistic();
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (!ended)
        {
            taps++;
            if (active)
                tweener.Complete();

            float delta = taps / tapsNeeded;

            if (taps >= nextStep) SpawnCoins();

            tweener = transform.DOPunchRotation(new Vector3(0f, 0f, delta * 90f * dir), 0.5f);
            active = true;
            timer = 0.5f;
            dir = -dir;

            if (taps >= tapsNeeded)
            {
                ended = true;
                endMenu.SetActive(true);

                Tweener coinsTweener = DOTween.To(() => coinsValue, x => coinsValue = x, coinsGavered, 0.75f);
                coinsTweener.onUpdate += () => { coinsText.text = coinsValue.ToString("F0"); };

                Tweener distanceTweener = DOTween.To(() => distance, x => distance = x, distanceTarget, 0.75f);
                distanceTweener.onUpdate += () => { distanceText.text = distance.ToString("F0") + " <color=red>m</color>"; };

                foreach (var t in coins)
                {
                    Tweener a = t.DOMove(coinsTarget.position, 0.75f);

                    a.onComplete += () => { Destroy(t.gameObject); };
                }
                animator.SetInteger("level", 4);
            }
        }
    }

    private void CountStatistic()
    {
        if (coinsGavered > coinsObj.maxCoins)
            coinsObj.maxCoins = (int)coinsGavered;
        if(distanceTarget > coinsObj.maxDistance)
            coinsObj.maxDistance = (int)distanceTarget;

        string primalPath = Application.persistentDataPath;

        SaveDataAsync(primalPath + "ShopSaves", primalPath + "citiesSaves");
    }

    private async void SaveDataAsync(string shopPath, string mainPath)
    {
        await Task.Run(() =>
        {
            dataLoader.GetDataLoadedAsync(shopPath, out ShopDataHandler shopData);
            shopData ??= new();
            shopData.coins += (int)coinsGavered;

            dataLoader.GetDataLoadedAsync(mainPath, out CitiesDataHandler mainData);
            mainData ??= new();

            mainData.cities[coinsObj.levelNumber].bestCollect = coinsObj.maxCoins;
            mainData.cities[coinsObj.levelNumber].bestDistance = coinsObj.maxDistance;
            mainData.cities[coinsObj.levelNumber].distanceTillBoss = (int)coinsObj.distanceTillBoss;

            dataLoader.SaveDataAsync(shopPath, shopData);
            dataLoader.SaveDataAsync(mainPath, mainData);
        });
    }

    private void SpawnCoins()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject t = Instantiate(coin, transform.position, Quaternion.identity, transform.parent);
            
            Rigidbody2D rb = t.GetComponent<Rigidbody2D>();

            coins[(steps - 1) * 5 + i] = t.transform;
            
            rb.velocity = new Vector2 (Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            rb.angularVelocity = Random.Range(rotRange.x, rotRange.y);
        }

        animator.SetInteger("level", steps);
        steps++;
        nextStep = (int)(tapsNeeded * 0.3f * steps);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            active = false;
        }
    }

}

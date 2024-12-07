using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private PigeonSpawner counterHandler;
    [SerializeField] private float distanceToSpawn;
    [SerializeField] private bool reduceTime;
    [SerializeField] private List<Obstacle> hiddenObjects;

    private float distancePassed;
    private DistanceCounter counter;
    private int lastObstacleIndex = -1;

    private void OnEnable()
    {
        PigeonHealth.PigeonDied += StopGame;
    }
    private void OnDisable()
    {
        PigeonHealth.PigeonDied -= StopGame;
    }

    private void Start()
    {
        counter = counterHandler.Counter;
    }

    void StopGame()
    {
        enabled = false;
    }

    private void Awake()
    {
        Obstacle[] temp = hiddenObjects.ToArray();
        hiddenObjects.Clear();

        for(int i = 0; i < 3; i++)
        {
            foreach(var t in temp)
            {
                Obstacle g = Instantiate(t);
                
                g.gameObject.SetActive(false);
                g.SelfAwake(this);

                hiddenObjects.Add(g);
            }
        }
    }

    private void Update()
    {
        if(distancePassed >= distanceToSpawn && hiddenObjects.Count > 0)
        {
            distancePassed = 0f;
            int rng = Random.Range(0, hiddenObjects.Count);

            for ( int i = 0; i < hiddenObjects.Count; i++)
            {
                if (hiddenObjects[rng].Index == lastObstacleIndex)
                {
                    if (rng < hiddenObjects.Count - 1) rng++;
                    else rng = 0;
                }
                else break;
            }

            Obstacle t = hiddenObjects[rng];
            lastObstacleIndex = t.Index;

            t.gameObject.SetActive(true);
            
            hiddenObjects.Remove(t);

            t.Initialize(new Vector3(transform.position.x + 10f, 0f, 0f));
        } 
    }

    private void LateUpdate()
    {
        distancePassed += counter.LastDelta;
    }

    public void SendForReuse(Obstacle ob)
    {
        ob.gameObject.SetActive(false);

        hiddenObjects.Add(ob);
    }
}

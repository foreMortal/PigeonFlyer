using System.Collections.Generic;
using UnityEngine;

public class EggsManager : MonoBehaviour
{
    [SerializeField] private EggProjectile eggPrefab;
    [SerializeField] private bool CheckForCollision = true;
    [SerializeField] private LayerMask mask;

    private ContactFilter2D filter;
    private List<EggProjectile> activeEggs = new List<EggProjectile>();
    private List<EggProjectile> hiddenEggs = new List<EggProjectile>();
    private Collider2D[] results = new Collider2D[1];
    private bool active;

    private void Awake()
    {
        active = true;

        filter.SetLayerMask(mask);
    }

    private void Update()
    {
        if (active)
        {
            foreach (var e in activeEggs)
            {
                e.transform.position = Vector3.MoveTowards(e.transform.position, e.transform.position + e.FlyingVector, e.Speed * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        if (CheckForCollision)
        {
            for (int i = 0; i < activeEggs.Count; i++)
            {
                if (Physics2D.OverlapCircle(activeEggs[i].transform.position + activeEggs[i].Offset, activeEggs[i].Radius, filter, results) > 0)
                {
                    results[0].GetComponent<ICanGetHitted>().GetHitted(activeEggs[i].Damage);
                    activeEggs[i].GoForReuse();
                }
            }
        }
    }

    public void ThrowEgg(Vector3 vector)
    {
        if (hiddenEggs.Count > 0)
        {
            hiddenEggs[0].gameObject.SetActive(true);
            hiddenEggs[0].transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            hiddenEggs[0].FlyingVector = vector;

            activeEggs.Add(hiddenEggs[0]);
            hiddenEggs.RemoveAt(0);
        }
        else
        {
            EggProjectile e = Instantiate(eggPrefab, transform.position, Quaternion.identity);
            e.SelfAwake(this);
            e.FlyingVector = vector;
            activeEggs.Add(e);
        }
    }

    public void ChangeGameState(bool state)
    {
        active = state;
    }

    public void SendForReuse(EggProjectile ob)
    {
        ob.gameObject.SetActive(false);

        activeEggs.Remove(ob);
        hiddenEggs.Add(ob);
    }
}

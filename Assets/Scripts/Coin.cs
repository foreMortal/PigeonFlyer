using UnityEngine;

public class Coin : InitializableObject, ICollectables, IReusable
{
    [SerializeField] private int value;
    private CoinsManager manager;

    public void SelfAwake(CoinsManager manager)
    {
        this.manager = manager;
    }

    public override void Initialize(Vector3 SpawnPosition)
    {
        base.Initialize(SpawnPosition);
    }

    public void GoForReuse()
    {
        manager.SendForReuse(this);
    }

    public void Collect(CoinsCollector c)
    {
        c.Coins += value;
        c.UpdateCoinsUI();
        manager.SendForReuse(this);
    }
}

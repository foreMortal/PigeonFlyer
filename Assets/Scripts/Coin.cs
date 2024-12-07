using UnityEngine;

public class Coin : InitializableObject, ICollectables, IReusable
{
    [SerializeField] private int value;
    [SerializeField] private SoundManager sound;

    private bool end;
    private float endTimer;
    private CoinsManager manager;
    private SpriteRenderer sprite;

    public void SelfAwake(CoinsManager manager)
    {
        sprite = GetComponent<SpriteRenderer>();
        this.manager = manager;
    }

    public override void Initialize(Vector3 SpawnPosition)
    {
        base.Initialize(SpawnPosition);
        sprite.enabled = true;
    }

    private void Update()
    {
        if (end)
        {
            if(endTimer < Time.time)
            {
                end = false;
                manager.SendForReuse(this);
            }
        }
    }

    public void GoForReuse()
    {
        manager.SendForReuse(this);
    }

    public void Collect(CoinsCollector c)
    {
        if (!end)
        {
            end = true;
            endTimer = Time.time + 1f;
            sprite.enabled = false;
            sound.Play();

            c.Coins += value;
            c.UpdateCoinsUI();
        }
    }
}

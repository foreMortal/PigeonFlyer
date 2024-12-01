using UnityEngine;

public class Obstacle : InitializableObject, IReusable
{
    [SerializeField] private int index;

    private ObstacleManager manager;
    public int Index { get { return index; } }

    public override void Initialize(Vector3 SpawnPosition)
    {
        base.Initialize(SpawnPosition);
    }

    public void SelfAwake(ObstacleManager m)
    {
        manager = m;
    }

    public void GoForReuse()
    {
        manager.SendForReuse(this);
    }
}

public enum ObstacleType
{
    StaticObstacle,
    MovingObstacle,
}

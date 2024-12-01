using UnityEngine;

public class MovingObstacle : Obstacle
{
    [Space]
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private bool randomDirection;
    [SerializeField] private Vector2 minRandomDirection, maxRandomDirection;

    public override void Initialize(Vector3 SpawnPosition)
    {
        base.Initialize(SpawnPosition);

        if (randomDirection)
        {
            float x = Random.Range(minRandomDirection.x, maxRandomDirection.x);
            float y = Random.Range(minRandomDirection.y, maxRandomDirection.y);

            direction += new Vector3(x, y);
        }

        direction *= speed;
    }

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
}

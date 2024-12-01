using UnityEngine;

public class ObstacleHealth : MonoBehaviour, ICanGetHitted
{
    [SerializeField] private float health;
    [SerializeField] private bool invincible;
    private IReusable reuse;

    private void Awake()
    {
        reuse = GetComponent<IReusable>();
    }

    public void GetHitted(float damage)
    {
        if (!invincible)
        {
            health -= damage;

            if(health <= 0f) reuse.GoForReuse();
        }
    }
}

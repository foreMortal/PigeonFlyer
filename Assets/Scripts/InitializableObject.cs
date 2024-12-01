using UnityEngine;

public class InitializableObject : MonoBehaviour
{
    [Tooltip("Offset applied to object instantiating position")]
    [SerializeField] private Vector3 startOffset;
    [SerializeField] private bool randomStartPosition;
    [Tooltip("Range of random offset applied to startOffset in order: minValue -> maxValue")]
    [SerializeField] private Vector2 minRange, maxRange;

    public virtual void Initialize(Vector3 SpawnPosition)
    {
        transform.position = SpawnPosition;

        if (!randomStartPosition)
        {
            transform.position += startOffset;
        }
        else
        {
            float x = Random.Range(minRange.x, maxRange.x);
            float y = Random.Range(minRange.y, maxRange.y);

            transform.position += startOffset + new Vector3(x, y, 0f);
        }
    }
}

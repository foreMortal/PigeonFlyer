using UnityEngine;

public class ParallarxBackground : CameraLateUpdateRoutine
{
    [SerializeField] private Transform background;
    [SerializeField] private float amountOfParallax;

    private float startingPos, lengthOfSprite; 

    private void Start()
    {
        startingPos = background.position.x;
        lengthOfSprite = background.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;
    }

    public override void PerformRoutine()
    {
        Vector3 position = transform.position;
        float temp = position.x * (1 - amountOfParallax);
        float distance = position.x * amountOfParallax;

        Vector3 newPosition = new Vector3(startingPos + distance, background.position.y, background.position.z);

        background.position = newPosition;

        if (temp > startingPos + (lengthOfSprite / 2))
        {
            startingPos += lengthOfSprite;
        }
        else if (temp < startingPos - (lengthOfSprite / 2))
        {
            startingPos -= lengthOfSprite;
        }
    }
}

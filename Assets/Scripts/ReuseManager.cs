using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReuseManager : MonoBehaviour
{
    [SerializeField] private LayerMask objectsForReuse;

    private ContactFilter2D filter;

    private Vector3[] collectorPositions = new Vector3[3] { new Vector3(0f, 10f), new Vector3(-15f, 0f), new Vector3(20f, 0f) };
    private Vector3[] collectorSizes = new Vector3[3] { new Vector2(30f, 4f), new Vector2(4f, 20f), new Vector2(4f, 20f) };
    private Collider2D[] contacts = new Collider2D[1];

    private void Awake()
    {
        filter.SetLayerMask(objectsForReuse);
        filter.useTriggers = true;
    }
    
    private void FixedUpdate()
    {
        int idx = Time.frameCount % 3;
        if (Physics2D.OverlapBox(transform.position + collectorPositions[idx], collectorSizes[idx], 0f, filter, contacts) > 0)
        {
            IReusable c = contacts[0].GetComponent<IReusable>();

            c.GoForReuse();
        }
    }
}

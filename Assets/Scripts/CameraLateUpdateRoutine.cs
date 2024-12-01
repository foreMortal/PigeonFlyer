using UnityEngine;

public abstract class CameraLateUpdateRoutine : MonoBehaviour
{
    [SerializeField] private int performingIndex;

    public int PerformingIndex { get { return performingIndex; } }

    public abstract void PerformRoutine();
}

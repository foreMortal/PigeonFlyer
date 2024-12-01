using UnityEngine;

public class EggProjectile : MonoBehaviour, IReusable
{
    [SerializeField] private float eggDamage, eggSpeed;
    [SerializeField] private Vector3 offset, flyingVector;
    [SerializeField] private float radius;

    private EggsManager manager;

    public float Radius { get { return radius; } }
    public Vector3 Offset { get { return offset; } }
    public Vector3 FlyingVector { get { return flyingVector; } set { flyingVector = value; } }
    public float Damage { get { return eggDamage; } }
    public float Speed { get { return eggSpeed; } }

    public void SelfAwake(EggsManager m)
    {
        manager = m;
    }
    public void GoForReuse()
    {
        manager.SendForReuse(this);
    }
}

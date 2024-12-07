using UnityEngine;

public class CameraFollowPlayer : CameraLateUpdateRoutine
{
    [SerializeField] private PigeonSpawner playerHandler;
    [Tooltip("offset between camera and player")]
    [SerializeField] private Vector3 offset;

    private Transform player;
    private CameraManager manager;
    private bool active;

    private void OnEnable()
    {
        manager.playerDied += PlayerDied;
    }
    private void OnDisable()
    {
        manager.playerDied -= PlayerDied;
    }

    private void Awake()
    {
        manager = GetComponent<CameraManager>();
        active = true;
    }

    private void Start()
    {
        player = playerHandler.PlayerTransform;
    }

    private void PlayerDied()
    {
        active = false;
    }

    public override void PerformRoutine()
    {
        if(active)
            transform.position = new Vector3(player.position.x, transform.position.y, -10f) - offset;
    }
}

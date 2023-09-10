using UnityEngine;

public class ShootingController : TimerTriggerController, IUpdatable
{
    [SerializeField]
    private Transform shotingPoint;

    private ObjectPoolingManager _poolingManager;
    private const string BULLET_TAG = GameObjectTagsConstants.BULLET;

    protected override float Interval => 1.0f;

    protected override void Awake()
    {
        base.Awake();

        _poolingManager = ObjectPoolingManager.Instance;
    }

    public void OnUpdate()
    {
        if (!TriggerActive) return;
        if (!gameObject.activeInHierarchy) return;
        if (!shotingPoint)
        {
            Debug.LogError($"Shoting point of {name} is not set in the inspector!");
            return;
        }

        var bullet = _poolingManager.GetFromPool(BULLET_TAG).GetComponent<BulletMovementController>();

        bullet.StartMovementFrom(shotingPoint.position, shotingPoint.rotation);

        ResetTrigger();
    }
}

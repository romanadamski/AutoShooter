using UnityEngine;

public class ShootingController : TimerTriggerController, IUpdatable
{
    [SerializeField]
    private Transform shotingPoint;

    protected override float Interval => 1.0f;

    public void OnUpdate()
    {
        if (!TriggerActive) return;
        if (!gameObject.activeInHierarchy) return;
        if (!shotingPoint)
        {
            Debug.LogError($"Shoting point of {name} is not set in the inspector!");
            return;
        }

        var bullet = ObjectPoolingManager.Instance.GetFromPool(GameObjectTagsConstants.BULLET).GetComponent<BulletMovementController>();

        bullet.StartMovementFrom(shotingPoint.position, shotingPoint.rotation);

        ResetTrigger();
    }
}

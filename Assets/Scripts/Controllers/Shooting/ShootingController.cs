using UnityEngine;

[RequireComponent(typeof(TimeShootingTrigger))]
public class ShootingController : MonoBehaviour, IUpdatable
{
    [SerializeField]
    private Transform shotingPoint;
    private TimeShootingTrigger _shootingTrigger;

    private void Awake()
    {
        _shootingTrigger = GetComponent<TimeShootingTrigger>();
    }

    public void OnUpdate()
    {
        if (!_shootingTrigger.TriggerActive) return;
        if (!gameObject.activeInHierarchy) return;
        if (!shotingPoint)
        {
            Debug.LogError($"Shoting point of {name} is not set in inspector!");
            return;
        }

        var bullet = ObjectPoolingManager.Instance.GetFromPool(GameObjectTagsConstants.BULLET).GetComponent<ResizableController>();
        bullet.transform.rotation = shotingPoint.rotation;
        bullet.transform.position = shotingPoint.position + bullet.transform.up * bullet.Bounds.size.y;
        bullet.gameObject.SetActive(true);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.up * GameSettingsManager.Instance.Settings.BulletMovementSpeed);

        _shootingTrigger.ResetTrigger();
    }
}

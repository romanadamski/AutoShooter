using UnityEngine;

public class BulletMovementController : MonoBehaviour, IUpdatable
{
    private ResizableController _resizableController;

    private void Awake()
    {
        _resizableController = GetComponent<ResizableController>();
    }

    public void StartMovementFrom(Vector3 position, Quaternion rotation)
    {
        transform.rotation = rotation;
        transform.position = position + transform.up * _resizableController.Bounds.size.y;
        gameObject.SetActive(true);
        GetComponent<Rigidbody>().AddForce(transform.up * GameSettingsManager.Instance.Settings.BulletMovementSpeed);

        BulletMovementManager.Instance.AddBullet(this);
    }

    public void OnUpdate()
    {
        if (!gameObject.activeInHierarchy) return;

        var position = transform.position;
        var gameBounds = GameLauncher.Instance.GamePlane.GameBounds;

        if (position.x > gameBounds.max.x || position.z > gameBounds.max.z
            || position.x < gameBounds.min.x || position.z < gameBounds.min.z)
        {
            ObjectPoolingManager.Instance.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
        }
    }
}

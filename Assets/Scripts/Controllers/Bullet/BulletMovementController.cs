using UnityEngine;

public class BulletMovementController : MonoBehaviour, IUpdatable
{
    private ResizableController _resizableController;
    private BasePoolableController _poolableController;
    private Rigidbody _rigidbody;
    private GameSettingsScriptableObject _settings;

    private void Awake()
    {
        _resizableController = GetComponent<ResizableController>();
        _poolableController = GetComponent<BasePoolableController>();
        _rigidbody = GetComponent<Rigidbody>();
        _settings = GameSettingsManager.Instance.Settings;
    }

    public void StartMovementFrom(Vector3 position, Quaternion rotation)
    {
        transform.rotation = rotation;
        transform.position = position + transform.up * _resizableController.Bounds.size.z / 2;
        gameObject.SetActive(true);

        _rigidbody.velocity = transform.up * _settings.BulletMovementSpeed;

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
            ObjectPoolingManager.Instance.ReturnToPool(_poolableController);
        }
    }
}

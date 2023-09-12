using UnityEngine;

public class BulletPoolableController : BasePoolableController
{
    private Rigidbody _rigidbody;
    private int id;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        id = GetInstanceID();
    }

    public override void OnReturnToPool()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        BulletMovementManager.Instance.RemoveBullet(id);
    }
}

using UnityEngine;

public class BulletPoolableController : BasePoolableController
{
    private Rigidbody _rigidbody;
    private IUpdatable _updatable;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _updatable = GetComponent<IUpdatable>();
    }

    public override void OnReturnToPool()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        BulletMovementManager.Instance.RemoveBullet(_updatable);
    }
}

using UnityEngine;

public class BaseBulletMovementController : BaseMovementController
{
    protected override void MoveObject(Vector3 direction)
    {
        _rigidbody2D.velocity = direction * GameSettingsManager.Instance.Settings.BaseBulletMovementSpeed * _speedMultiplier;
    }
}

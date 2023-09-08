using UnityEngine;

[RequireComponent(typeof(BaseRotateTrigger))]
public class RotateController : MonoBehaviour, IUpdatable
{
    private BaseRotateTrigger _rotateTrigger;
    private Quaternion _rotation;

    private void Awake()
    {
        _rotateTrigger = GetComponent<BaseRotateTrigger>();
    }

    public void OnUpdate()
    {
        if (!_rotateTrigger.RotateTriggerActive) return;
        _rotation = Quaternion.Lerp(
                transform.rotation,
                _rotateTrigger.RotationValue,
                Time.deltaTime * GameSettingsManager.Instance.Settings.ShooterRotationSpeed);

        DoRotate();
    }

    private void DoRotate()
    {
        transform.rotation = _rotation;
        if (transform.rotation.Equals(_rotateTrigger.RotationValue))
        {
            _rotateTrigger.ResetRotateTrigger();
        }
    }
}

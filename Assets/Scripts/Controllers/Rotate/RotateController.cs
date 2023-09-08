using UnityEngine;

[RequireComponent(typeof(TimerRotateTrigger))]
public class RotateController : MonoBehaviour, IUpdatable
{
    private TimerRotateTrigger _rotateTrigger;
    private Quaternion _rotation;

    private void Awake()
    {
        _rotateTrigger = GetComponent<TimerRotateTrigger>();
    }

    public void OnUpdate()
    {
        if (!_rotateTrigger.TriggerActive) return;
        if (!gameObject.activeInHierarchy) return;

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
            _rotateTrigger.ResetTrigger();
        }
    }
}

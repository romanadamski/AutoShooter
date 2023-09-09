using UnityEngine;

public class RotateController : BaseTriggerController, IUpdatable
{
    private Quaternion _rotation;

    protected override float Interval => ShooterRandomizeHelper.GetRandomTimeFrequency();

    public Quaternion RotationValue { get; private set; }

    protected override void OnTimerElapsed()
    {
        RotationValue = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    public void OnUpdate()
    {
        if (!TriggerActive) return;
        if (!gameObject.activeInHierarchy) return;

        _rotation = Quaternion.Lerp(
                transform.rotation,
                RotationValue,
                Time.deltaTime * GameSettingsManager.Instance.Settings.ShooterRotationSpeed);

        DoRotate();
    }

    private void DoRotate()
    {
        transform.rotation = _rotation;
        if (transform.rotation.Equals(RotationValue))
        {
            ResetTrigger();
        }
    }
}

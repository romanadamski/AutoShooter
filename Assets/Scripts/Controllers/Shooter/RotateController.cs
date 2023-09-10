using UnityEngine;

public class RotateController : TimerTriggerController, IUpdatable
{
    private GameSettingsScriptableObject _settings;

    protected override float Interval => GetRandomTimeFrequency();

    public Quaternion RotationValue { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        _settings = GameSettingsManager.Instance.Settings;
    }

    protected override void OnTimerElapsed()
    {
        RotationValue = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    public void OnUpdate()
    {
        if (!TriggerActive) return;
        if (!gameObject.activeInHierarchy) return;

        transform.rotation = Quaternion.Lerp(
                transform.rotation,
                RotationValue,
                Time.deltaTime * _settings.ShooterRotationSpeed);

        if (transform.rotation.Equals(RotationValue))
        {
            ResetTrigger();
        }
    }

    private float GetRandomTimeFrequency()
    {
        return Random.Range(
            _settings.RotatingTimeFrequency.Item1,
            _settings.RotatingTimeFrequency.Item2);
    }
}

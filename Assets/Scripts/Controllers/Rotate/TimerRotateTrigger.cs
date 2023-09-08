using UnityEngine;

public class TimerRotateTrigger : BaseTriggerController
{
    public Quaternion RotationValue { get; private set; }
    protected override float Interval => ShooterRandomizeHelper.GetRandomTimeFrequency();

    protected override void OnTimerElapsed()
    {
        RotationValue = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }
}

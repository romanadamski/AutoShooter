using UnityEngine;

public class ShooterRandomizeHelper
{
    public float GetRandomTimeFrequency()
    {
        return Random.Range(
            GameSettingsManager.Instance.Settings.RotatingTimeFrequency.Item1,
            GameSettingsManager.Instance.Settings.RotatingTimeFrequency.Item2);
    }
}

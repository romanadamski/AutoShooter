using UnityEngine;

public static class ShooterRandomizeHelper
{
    public static float GetRandomTimeFrequency()
    {
        return Random.Range(
            GameSettingsManager.Instance.Settings.RotatingTimeFrequency.Item1,
            GameSettingsManager.Instance.Settings.RotatingTimeFrequency.Item2);
    }
}

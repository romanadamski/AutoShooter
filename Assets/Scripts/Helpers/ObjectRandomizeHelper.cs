using UnityEngine;

public class ObjectRandomizeHelper
{
    private const float DIRECTION_RANDOMIZE_RANGE = 0.8f;

    public float GetRandomSpeed()
    {
        return Random.Range(
            LevelSettingsManager.Instance.CurrentLevel.SpeedRange.Item1,
            LevelSettingsManager.Instance.CurrentLevel.SpeedRange.Item2);
    }

    public float GetRandomFrequency()
    {
        return Random.Range(
            LevelSettingsManager.Instance.CurrentLevel.ReleasingFrequency.Item1,
            LevelSettingsManager.Instance.CurrentLevel.ReleasingFrequency.Item2);
    }

    public Vector2 GetRandomDirectionDependsOnPosition(Transform shooter)
    {
        var direction = ScreenManager.Instance.GetObjectPositionRelativeToScreen(shooter);
        var randomX = 0.0f;
        var randomY = 0.0f;

        if (direction.Equals(ScreenDirectionEnum.SCREEN))
        {
            randomX = Random.Range(-DIRECTION_RANDOMIZE_RANGE, DIRECTION_RANDOMIZE_RANGE);
            randomY = Random.Range(-DIRECTION_RANDOMIZE_RANGE, DIRECTION_RANDOMIZE_RANGE);
        }
        else
        {
            if (direction.HasFlag(ScreenDirectionEnum.DOWN))
            {
                randomX += Random.Range(-DIRECTION_RANDOMIZE_RANGE, DIRECTION_RANDOMIZE_RANGE);
                randomY += 1;
            }
            else if (direction.HasFlag(ScreenDirectionEnum.UP))
            {
                randomX += Random.Range(-DIRECTION_RANDOMIZE_RANGE, DIRECTION_RANDOMIZE_RANGE);
                randomY += -1;
            }

            if (direction.HasFlag(ScreenDirectionEnum.LEFT))
            {
                randomX += 1;
                randomY += Random.Range(-DIRECTION_RANDOMIZE_RANGE, DIRECTION_RANDOMIZE_RANGE);
            }
            else if (direction.HasFlag(ScreenDirectionEnum.RIGHT))
            {
                randomX += -1;
                randomY += Random.Range(-DIRECTION_RANDOMIZE_RANGE, DIRECTION_RANDOMIZE_RANGE);
            }
        }

        return new Vector2(randomX, randomY).normalized;
    }

    public Vector2 GetRandomPositionOutsideScreen()
    {
        var values = System.Enum.GetValues(typeof(ScreenDirectionEnum));
        var randomDirection = (ScreenDirectionEnum)values.GetValue(Random.Range(1, values.Length));

        var randomX = 0.0f;
        var randomY = 0.0f;

        switch (randomDirection)
        {
            case ScreenDirectionEnum.DOWN:
                randomX = GetRandomXScreenPosition();
                randomY = -1;
                break;
            case ScreenDirectionEnum.UP:
                randomX = GetRandomXScreenPosition();
                randomY = Screen.height + 1;
                break;
            case ScreenDirectionEnum.RIGHT:
                randomX = Screen.width + 1;
                randomY = GetRandomYScreenPosition();
                break;
            case ScreenDirectionEnum.LEFT:
                randomX = -1;
                randomY = GetRandomYScreenPosition();
                break;
        }

        return Camera.main.ScreenToWorldPoint(new Vector2(randomX, randomY));
    }

    private float GetRandomXScreenPosition()
    {
        return Random.Range(-1.0f, Screen.width + 1);
    }

    private float GetRandomYScreenPosition()
    {
        return Random.Range(-1.0f, Screen.height + 1);
    }
}

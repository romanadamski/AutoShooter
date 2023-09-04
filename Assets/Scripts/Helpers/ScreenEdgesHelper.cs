 using UnityEngine;

public class ScreenEdgesHelper
{
    /// <summary>
    /// Show object at opposite side of scrren after crossing screen edge
    /// </summary>
    /// <param name="objectTransform">Object which crossing screen's edge</param>
    public void HandleScreenEdgeCrossing(Transform objectTransform)
    {
        var direction =  GetObjectPositionRelativeToScreen(objectTransform);
        var screenPos = Camera.main.WorldToScreenPoint(objectTransform.position);
        if (direction.HasFlag(ScreenDirectionEnum.DOWN))
        {
            objectTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(
                screenPos.x,
                Screen.height,
                screenPos.z));
        }
        else if (direction.HasFlag(ScreenDirectionEnum.UP))
        {
            objectTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(
                screenPos.x,
                0,
                screenPos.z));
        }

        if (direction.HasFlag(ScreenDirectionEnum.LEFT))
        {
            objectTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(
                Screen.width,
                screenPos.y,
                screenPos.z));
        }
        else if (direction.HasFlag(ScreenDirectionEnum.RIGHT))
        {
            objectTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(
                0,
                screenPos.y,
                screenPos.z));
        }
    }

    /// <summary>
    /// Show object at opposite side of scrren after crossing screen edge
    /// </summary>
    /// <param name="objectTransform">Object which crossing screen's edge</param>
    public ScreenDirectionEnum GetObjectPositionRelativeToScreen(Transform objectTransform)
    {
        ScreenDirectionEnum direction = ScreenDirectionEnum.SCREEN;
        var screenPos = Camera.main.WorldToScreenPoint(objectTransform.position);

        if (screenPos.y < 0)
        {
            direction |= ScreenDirectionEnum.DOWN;
        }
        else if (screenPos.y > Screen.height)
        {
            direction |= ScreenDirectionEnum.UP;
        }

        if (screenPos.x < 0)
        {
            direction |= ScreenDirectionEnum.LEFT;
        }
        else if (screenPos.x > Screen.width)
        {
            direction |= ScreenDirectionEnum.RIGHT;
        }
        return direction;
    }
}

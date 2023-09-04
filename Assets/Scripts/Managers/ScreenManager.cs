using UnityEngine;

public class ScreenManager : BaseManager<ScreenManager>
{
    private ScreenEdgesHelper _screenEdgesHelper;

    private void Start()
    {
        _screenEdgesHelper = new ScreenEdgesHelper();
    }

    public void HandleScreenEdgeCrossing(Transform transform)
    {
        _screenEdgesHelper.HandleScreenEdgeCrossing(transform);
    }

    public ScreenDirectionEnum GetObjectPositionRelativeToScreen(Transform transform)
    {
        return _screenEdgesHelper.GetObjectPositionRelativeToScreen(transform);
    }
}

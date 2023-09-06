using UnityEngine;

public class GameLauncher : BaseManager<GameLauncher>
{
    [SerializeField]
    private BaseGamePlane gamePlane;
    public BaseGamePlane GamePlane => gamePlane;

    [SerializeField]
    private Canvas canvas;
    public Canvas Canvas => canvas;
}

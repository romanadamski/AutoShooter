using UnityEngine;

public class GameLauncher : BaseManager<GameLauncher>
{
    [SerializeField]
    private GameObject gamePlane;
    public GameObject GamePlane => gamePlane;

    [SerializeField]
    private Canvas canvas;
    public Canvas Canvas => canvas;

    private void Awake()
    {
    }
}

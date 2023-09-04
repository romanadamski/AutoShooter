using UnityEngine;

public abstract class BasePoolableController : MonoBehaviour
{
    [SerializeField]
    private int selectedTypeIndex;

    public string PoolableType;
}

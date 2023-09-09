using UnityEngine;

public abstract class BaseGamePlane : MonoBehaviour
{
    public abstract void SpawnGameplayObjects();
    public abstract Bounds GameBounds { get; }
}

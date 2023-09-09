using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGamePlane : MonoBehaviour
{
    public abstract void SpawnGameplayObjects();
    public abstract Bounds GameBounds { get; }
}

using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BaseCollisionController : MonoBehaviour
{
    public event Action<Collider> TriggerEnter;
    public event Action<Collider> TriggerExit;

    private void OnTriggerEnter(Collider collision)
    {
        if (!gameObject.activeInHierarchy) return;

        OnTriggerStart(collision);
    }

    private void OnTriggerExit(Collider collision)
    {
        OnTriggerEnd(collision);
    }

    private void OnTriggerStart(Collider collision)
    {
        TriggerEnter?.Invoke(collision);
    }

    private void OnTriggerEnd(Collider collision)
    {
        TriggerExit?.Invoke(collision);
    }
}

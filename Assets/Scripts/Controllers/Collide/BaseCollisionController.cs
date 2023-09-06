using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BaseCollisionController : MonoBehaviour
{
    public event Action<Collision> CollisionEnter;
    public event Action<Collision> CollisionExit;
    public event Action<Collider> TriggerEnter;
    public event Action<Collider> TriggerExit;

    private void OnCollisionEnter(Collision collision)
    {
        if (!gameObject.activeSelf) return;
        
        OnCollideStart(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        OnCollideEnd(collision);
    }

    private void OnCollideStart(Collision collision)
    {
        CollisionEnter?.Invoke(collision);
    }

    private void OnCollideEnd(Collision collision)
    {
        CollisionExit?.Invoke(collision);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!gameObject.activeSelf) return;

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

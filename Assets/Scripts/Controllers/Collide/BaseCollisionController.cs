using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BaseCollisionController : MonoBehaviour
{
    public event Action<Collision2D> CollisionEnter;
    public event Action<Collision2D> CollisionExit;
    public event Action<Collider2D> TriggerEnter;
    public event Action<Collider2D> TriggerExit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameObject.activeSelf) return;
        
        OnCollideStart(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnCollideEnd(collision);
    }

    private void OnCollideStart(Collision2D collision)
    {
        CollisionEnter?.Invoke(collision);
    }

    private void OnCollideEnd(Collision2D collision)
    {
        CollisionExit?.Invoke(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject.activeSelf) return;

        OnTriggerStart(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerEnd(collision);
    }

    private void OnTriggerStart(Collider2D collision)
    {
        TriggerEnter?.Invoke(collision);
    }

    private void OnTriggerEnd(Collider2D collision)
    {
        TriggerExit?.Invoke(collision);
    }
}

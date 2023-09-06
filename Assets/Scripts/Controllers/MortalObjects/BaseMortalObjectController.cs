using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BaseCollisionController))]
public abstract class BaseMortalObjectController : MonoBehaviour
{
    private string[] _enemyObjectsTags;
    private BaseCollisionController _collisionController;

    protected bool _enemyCollideExited = true;
    protected bool _enemyTriggerExited = true;

    protected virtual void OnCollisionWithEnemyEnter(Collision collision) { }
    protected virtual void OnCollisionWithEnemyExit(Collision collision) { }
    protected virtual void OnTriggerWithEnemyEnter(Collider collider) { }
    protected virtual void OnTriggerWithEnemyExit(Collider collider) { }
    protected virtual string[] GetEnemies() { return new string[] { }; }

    public uint LivesCount { get; protected set; }

    private void Awake()
    {
        _enemyObjectsTags = GetEnemies();
        _collisionController = GetComponent<BaseCollisionController>();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        _collisionController.CollisionEnter += CollisionEnter;
        _collisionController.CollisionExit += CollisionExit;
        _collisionController.TriggerEnter += TriggerEnter;
        _collisionController.TriggerExit += TriggerExit;
    }

    private void CollisionEnter(Collision collision)
    {
        if (_enemyCollideExited && _enemyObjectsTags.Contains(collision.transform.tag))
        {
            _enemyCollideExited = false;
            OnCollisionWithEnemyEnter(collision);
        }
    }

    private void CollisionExit(Collision collision)
    {
        if (_enemyObjectsTags.Contains(collision.transform.tag))
        {
            _enemyCollideExited = true;
            OnCollisionWithEnemyExit(collision);
        }
    }

    private void TriggerEnter(Collider collider)
    {
        if (_enemyTriggerExited && _enemyObjectsTags.Contains(collider.transform.tag))
        {
            _enemyTriggerExited = false;
            OnTriggerWithEnemyEnter(collider);
        }
    }

    private void TriggerExit(Collider collider)
    {
        if (_enemyObjectsTags.Contains(collider.transform.tag))
        {
            _enemyTriggerExited = true;
            OnTriggerWithEnemyExit(collider);
        }
    }

    protected void DecrementLive()
    {
        if (LivesCount > 0)
        {
            LivesCount--;
        }
    }

    private void UnsubscribeFromEvents()
    {
        _collisionController.CollisionEnter -= CollisionEnter;
        _collisionController.CollisionExit -= CollisionExit;
        _collisionController.TriggerEnter -= TriggerEnter;
        _collisionController.TriggerExit -= TriggerExit;
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}

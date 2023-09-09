using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BaseCollisionController))]
public abstract class BaseMortalObjectController : MonoBehaviour
{
    private string[] _enemyObjectsTags;
    private BaseCollisionController _collisionController;

    protected bool _enemyTriggerExited = true;

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

    private void OnEnable()
    {
        _enemyTriggerExited = true;
    }

    protected virtual void SubscribeToEvents()
    {
        _collisionController.TriggerEnter += TriggerEnter;
        _collisionController.TriggerExit += TriggerExit;
    }

    private void TriggerEnter(Collider collider)
    {
        if (!_enemyTriggerExited
            || !_enemyObjectsTags.Contains(collider.transform.tag)) return;

        _enemyTriggerExited = false;
        OnTriggerWithEnemyEnter(collider);
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
}

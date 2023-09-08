using UnityEngine;

//todo return to pool when far away from plane
public class MortalBulletController : BaseMortalObjectController
{
    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTagsConstants.SHOOTER };
    }

    protected override void OnTriggerWithEnemyEnter(Collider collider)
    {
        ObjectPoolingManager.Instance.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
    }
}

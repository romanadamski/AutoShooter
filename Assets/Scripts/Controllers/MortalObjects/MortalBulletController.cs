using UnityEngine;

public class MortalBulletController : BaseMortalObjectController
{
    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTagsConstants.OBJECT };
    }

    protected override void OnTriggerWithEnemyEnter(Collider collider)
    {
        ObjectPoolingManager.Instance.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
    }
}

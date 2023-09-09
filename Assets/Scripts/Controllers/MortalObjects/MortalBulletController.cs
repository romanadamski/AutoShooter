using UnityEngine;

public class MortalBulletController : BaseMortalObjectController
{
    protected override void OnTriggerWithEnemyEnter(Collider collider)
    {
        ObjectPoolingManager.Instance.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
    }
}

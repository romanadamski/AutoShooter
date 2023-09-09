using System;
using UnityEngine;
public class MortalShooterController : BaseMortalObjectController
{
    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTagsConstants.BULLET };
    }

    protected override void SubscribeToEvents()
    {
        base.SubscribeToEvents();

        EventsManager.Instance.GameplayStarted += SetLivesCount;
    }

    private void SetLivesCount()
    {
        LivesCount = GameSettingsManager.Instance.Settings.ShooterLives;
    }

    protected override void OnTriggerWithEnemyEnter(Collider collider)
    {
        DecrementLive();

        if (LivesCount > 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            ObjectPoolingManager.Instance.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
        }
        EventsManager.Instance.OnShooterShoted(LivesCount, gameObject);
    }
}

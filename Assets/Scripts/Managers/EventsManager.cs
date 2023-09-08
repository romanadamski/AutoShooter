using System;
using UnityEngine;

public class EventsManager : BaseManager<EventsManager>
{
    public event Action GameplayStarted;
    public void OnGameplayStarted()
    {
        GameplayStarted?.Invoke();
    }

    public event Action GameplayEnded;
    public void OnGameplayEnded()
    {
        GameplayEnded?.Invoke();
    }

    public event Action<uint, GameObject> ShooterShoted;
    public void OnShooterShoted(uint lives, GameObject shooter)
    {
        ShooterShoted?.Invoke(lives, shooter);
    }

    public event Action<uint> ShootersCountUpdated;
    public void OnShootersCountUpdated(uint score)
    {
        ShootersCountUpdated?.Invoke(score);
    }

    public event Action BulletFired;
    public void OnBulletFired()
    {
        BulletFired?.Invoke();
    }
}

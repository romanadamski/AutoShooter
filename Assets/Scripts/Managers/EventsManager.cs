using System;

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

    public event Action ObjectShotted;
    public void OnObjectShotted()
    {
        ObjectShotted?.Invoke();
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

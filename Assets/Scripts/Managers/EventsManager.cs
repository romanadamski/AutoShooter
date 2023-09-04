using System;

public class EventsManager : BaseManager<EventsManager>
{
    public event Action<uint> PlayerLoseLife;
    public void OnPlayerLoseLife(uint livesCount)
    {
        PlayerLoseLife?.Invoke(livesCount);
    }

    public event Action<uint> PlayerSpawned;
    public void OnPlayerSpawned(uint livesCount)
    {
        PlayerSpawned?.Invoke(livesCount);
    }
    
    public event Action ObjectShotted;
    public void OnObjectShotted()
    {
        ObjectShotted?.Invoke();
    }

    public event Action<uint> ScoreUpdated;
    public void OnScoreUpdated(uint score)
    {
        ScoreUpdated?.Invoke(score);
    }

    public event Action BulletFired;
    public void OnBulletFired()
    {
        BulletFired?.Invoke();
    }
}

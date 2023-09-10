using System;
using UnityEngine;

public enum TimerType
{
    Transient,
    Cached
}

public class TimerController : IUpdatable
{
    private bool _counting;
    private float _interval;
    private float _intervalCounting;
    private event Action _timerElapsed;
    private TimerManager _timerManager;
    
    public TimerType TimerType { get; private set; }

    public TimerController(Action timerElapsed, TimerType timerType)
    {
        _timerElapsed = timerElapsed;
        TimerType = timerType;

        _timerManager = TimerManager.Instance;

        _timerManager.AddTimer(this);
    }

    public void OnUpdate()
    {
        if (!_counting) return;

        if (_intervalCounting > 0)
        {
            _intervalCounting -= Time.deltaTime;
        }
        else
        {
            Stop();
            OnTimerElapsed();
        }
    }

    private void OnTimerElapsed()
    {
        _timerElapsed?.Invoke();
        OnTimerElapsedByType();
    }

    private void OnTimerElapsedByType()
    {
        switch (TimerType)
        {
            case TimerType.Transient:
                _timerManager.RemoveTimer(this);
                break;
            case TimerType.Cached:
                _intervalCounting = _interval;
                Start();
                break;
            default:
                break;
        }
    }

    public void Start()
    {
        _intervalCounting = _interval;
        _counting = true;
    }

    public void Stop() => _counting = false;

    public void SetInterval(float interval)
    {
        _interval = interval;
    }
}

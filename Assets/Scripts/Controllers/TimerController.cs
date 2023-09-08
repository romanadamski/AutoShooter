using System;
using UnityEngine;

public class TimerController : IUpdatable
{
    private bool _counting;
    private float _interval;
    private event Action _timerElapsed;

    public TimerController(Action timerElapsed)
    {
        _timerElapsed = timerElapsed;

        TimerManager.Instance.AddTimer(this);
    }

    public void OnUpdate()
    {
        if (!_counting) return;

        if (_interval > 0)
        {
            _interval -= Time.deltaTime;
        }
        else
        {
            StopCounting();
            OnTimerElapsed();
        }
    }

    private void OnTimerElapsed()
    {
        _timerElapsed?.Invoke();
    }

    public void StartCounting()
    {
        _counting = true;
    }

    public void StopCounting() => _counting = false;

    public void SetInterval(float interval)
    {
        _interval = interval;
    }
}

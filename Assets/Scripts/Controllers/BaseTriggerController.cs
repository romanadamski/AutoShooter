using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTriggerController : MonoBehaviour
{
    private TimerController _timerController;
    public bool TriggerActive { get; private set; }

    protected abstract float Interval { get; }

    private void Awake()
    {
        _timerController = new TimerController(TimerElapsed);
    }

    private void OnEnable()
    {
        StartCounting();
    }

    public void ResetTrigger()
    {
        TriggerActive = false;
    }

    private void TimerElapsed()
    {
        OnTimerElapsed();

        TriggerActive = true;
        StartCounting();
    }

    protected virtual void OnTimerElapsed() { }

    private void StartCounting()
    {
        _timerController.SetInterval(Interval);
        _timerController.StartCounting();
    }

    private void OnDisable()
    {
        _timerController.StopCounting();
    }
}

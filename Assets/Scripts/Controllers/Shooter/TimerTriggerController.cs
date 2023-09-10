using UnityEngine;

public abstract class TimerTriggerController : MonoBehaviour
{
    private TimerController _timerController;

    protected abstract float Interval { get; }

    public bool TriggerActive { get; private set; }

    protected virtual void Awake()
    {
        _timerController = new TimerController(TimerElapsed, TimerType.Cached);
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
    }

    protected virtual void OnTimerElapsed() { }

    private void StartCounting()
    {
        _timerController.SetInterval(Interval);
        _timerController.Start();
    }

    private void OnDisable()
    {
        _timerController.Stop();
    }
}

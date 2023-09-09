using System.Collections.Generic;
using System.Linq;

public class TimerManager : BaseManager<TimerManager>
{
    private List<TimerController> _cachedTimerControllers = new List<TimerController>();
    private List<TimerController> _transientTimerControllers = new List<TimerController>();

    private void Start()
    {
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.GameplayEnded += GameplayEnded;
    }

    private void GameplayEnded()
    {
        _transientTimerControllers.ForEach(timer => timer.Stop());
        _transientTimerControllers.Clear();
    }

    public void AddTimer(TimerController timer)
    {
        AddTimerByType(timer);
    }

    private void AddTimerByType(TimerController timer)
    {
        switch (timer.TimerType)
        {
            case TimerType.Transient:
                _transientTimerControllers.Add(timer);
                break;
            case TimerType.Cached:
                _cachedTimerControllers.Add(timer);
                break;
            default:
                break;
        }
    }

    public void RemoveTimer(TimerController timer)
    {
        _transientTimerControllers.Remove(timer);
    }

    private void Update()
    {
        _cachedTimerControllers.ForEach(timer => timer.OnUpdate());
        _transientTimerControllers.ToList().ForEach(timer => timer.OnUpdate());
    }
}

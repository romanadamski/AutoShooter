using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : BaseManager<TimerManager>
{
    List<TimerController> timerControllers = new List<TimerController>();

    public void AddTimer(TimerController timerController)
    {
        timerControllers.Add(timerController);
    }

    private void Update()
    {
        timerControllers.ForEach(timerController => timerController.OnUpdate());
    }
}

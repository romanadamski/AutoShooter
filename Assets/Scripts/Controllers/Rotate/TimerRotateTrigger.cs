using UnityEngine;

public class TimerRotateTrigger : BaseRotateTrigger, IUpdatable
{
    private ShooterRandomizeHelper _randomizeHelper;
    private float _rotationFrequency;
    private bool _counting;

    private void Awake()
    {
        _randomizeHelper = new ShooterRandomizeHelper();
    }

    public void OnUpdate()
    {
        if (!_counting)
        {
            StartCounting();
        }
        if (_counting)
        {
            if (_rotationFrequency > 0)
            {
                _rotationFrequency -= Time.deltaTime;
            }
            else
            {
                StopCounting();
                TimerElapsed();
            }
        }
    }

    private void TimerElapsed()
    {
        RotateTriggerActive = true;
        RotationValue = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    private void StartCounting()
    {
        _rotationFrequency = _randomizeHelper.GetRandomTimeFrequency();
        _counting = true;
    }

    private void StopCounting() => _counting = false;
}

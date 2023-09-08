using UnityEngine;

public abstract class BaseRotateTrigger : MonoBehaviour
{
    public bool RotateTriggerActive { get; protected set; }
    public Quaternion RotationValue { get; protected set; }

    public void ResetRotateTrigger()
    {
        RotateTriggerActive = false;
    }
}

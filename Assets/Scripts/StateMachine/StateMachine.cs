using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }

    public void SetState(State state)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        CurrentState = state;

        CurrentState.Enter();
    }

    private void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }

    public void Clear()
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }
        CurrentState = null;
    }
}

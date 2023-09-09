using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : BaseManager<GameplayManager>
{
    #region States

    private StateMachine _gameplayStateMachine;

    public GameplayState GameplayState { get; private set; }
    public GameOverState GameOverState { get; private set; }
    public EndGameplayState EndGameplayState { get; private set; }

    #endregion

    public uint CurrentScore { get; private set; }
    public bool DuringGameplayState => _gameplayStateMachine.CurrentState?.GetType() == typeof(GameplayState);

    private void Awake()
    {
        InitStates();
    }

    private void Start()
    {
        SubscribeToEvents();
    }

    private void InitStates()
    {
        _gameplayStateMachine = gameObject.AddComponent<StateMachine>();

        GameplayState = new GameplayState(_gameplayStateMachine);
        GameOverState = new GameOverState(_gameplayStateMachine);
        EndGameplayState = new EndGameplayState(_gameplayStateMachine);
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.ShooterShoted += (lives, shooter) => ShooterShoted(lives);
    }

    public void ClearGameplay()
    {
        ObjectPoolingManager.Instance.ReturnAllToPools();
        EventsManager.Instance.OnGameplayEnded();
    }

    private void ShooterShoted(uint lives)
    {
        if (lives == 0)
        {
            DecrementScore();
        }
    }

    private void StartCurrentLevel()
    {
        LevelSettingsManager.Instance.SetCurrentLevel();
        CurrentScore = LevelSettingsManager.Instance.CurrentLevel.ShootersCount;
        GameLauncher.Instance.GamePlane.SpawnGameplayObjects();
    }

    private void DecrementScore()
    {
        CurrentScore--;
        if (CurrentScore.Equals(GameSettingsManager.Instance.Settings.MinShootersCount))
        {
            SetGameOverState();
        }
        
        EventsManager.Instance.OnShootersCountUpdated(CurrentScore);
    }

    public void StartGameplay()
    {
        StartCurrentLevel();
        EventsManager.Instance.OnGameplayStarted();
    }

    public void SetGameplayState()
    {
        _gameplayStateMachine.SetState(GameplayState);
    }

    public void SetGameOverState()
    {
        _gameplayStateMachine.SetState(GameOverState);
    }

    public void SetEndGameplayState()
    {
        _gameplayStateMachine.SetState(EndGameplayState);
    }

    public void ClearGameplayStateMachine()
    {
        ClearGameplay();
        _gameplayStateMachine.Clear();
    }
}

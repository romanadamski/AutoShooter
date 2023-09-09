using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : BaseManager<GameplayManager>
{
    private List<GameObject> _playerObjects = new List<GameObject>();

    #region States

    private StateMachine _gameplayStateMachine;

    public GameplayState GameplayState { get; private set; }
    public WinState WinState { get; private set; }
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
        WinState = new WinState(_gameplayStateMachine);
        GameOverState = new GameOverState(_gameplayStateMachine);
        EndGameplayState = new EndGameplayState(_gameplayStateMachine);
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.ShooterShoted += (lives, shooter) => ShooterShoted(lives, shooter);
    }

    public void ClearGameplay()
    {
        ObjectPoolingManager.Instance.ReturnAllToPools();
        DestroyAllPlayerObjects();
        EventsManager.Instance.OnGameplayEnded();
    }

    private void ShooterShoted(uint lives, GameObject shooter)
    {
        if (lives == 0)
        {
            DecrementScore();
        }
    }

    private void StartCurrentLevel()
    {
        LevelSettingsManager.Instance.SetCurrentLevel();
        CurrentScore = LevelSettingsManager.Instance.CurrentLevel.ObjectsCount;
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
        ResumeGameplay();
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

    public void DestroyAllPlayerObjects()
    {
        foreach (var playerObject in _playerObjects)
        {
            Destroy(playerObject);
        }
        _playerObjects.Clear();
    }

    public void PauseGameplay()
    {
        Time.timeScale = 0;
    }

    public void ResumeGameplay()
    {
        Time.timeScale = 1;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : BaseManager<GameplayManager>
{
    private List<GameObject> _playerObjects = new List<GameObject>();

    #region States

    private StateMachine _gameplayStateMachine;

    public GameplayState GameplayState { get; private set; }
    public WinState WinState { get; private set; }
    public GameOverState GameOverMenu { get; private set; }
    public EndGameplayState EndGameplayState { get; private set; }

    #endregion

    public uint CurrentScore { get; private set; }

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
        GameOverMenu = new GameOverState(_gameplayStateMachine);
        EndGameplayState = new EndGameplayState(_gameplayStateMachine);
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.ObjectShotted += ObjectDead;
    }

    public void ClearGameplay()
    {

        ObjectPoolingManager.Instance.ReturnAllToPools();
        DestroyAllPlayerObjects();
    }

    private void ObjectDead()
    {
        DecrementScore();
    }

    public void StartCurrentLevel()
    {
        ResetScore();
        LevelSettingsManager.Instance.SetCurrentLevel();
        //todo spawn objects
    }

    private void DecrementScore()
    {
        CurrentScore -= 1;
        EventsManager.Instance.OnScoreUpdated(CurrentScore);
    }

    private void ResetScore()
    {
        CurrentScore = 0;
        EventsManager.Instance.OnScoreUpdated(CurrentScore);
    }

    public void StartGameplay()
    {
        ResumeGameplay();
        //todo spawn objects
    }

    public void SetGameplayState()
    {
        _gameplayStateMachine.SetState(GameplayState);
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

    private void UnsubscribeFromEvents()
    {
        if (!EventsManager.Instance) return;

        EventsManager.Instance.ObjectShotted -= ObjectDead;
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}

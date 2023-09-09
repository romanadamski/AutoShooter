using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameplayMenu : BaseMenu
{
    [SerializeField]
    private TextMeshProUGUI shooterCounter;
    [SerializeField]
    private Button menuButton;

    private void Awake()
    {
        SubscribeToEvents();
        menuButton.onClick.AddListener(OnGoToMainMenuClick);
    }

    private void SubscribeToEvents()
    {
        if (!EventsManager.Instance) return;

        EventsManager.Instance.GameplayStarted += GameplayStarted;
        EventsManager.Instance.ShootersCountUpdated += ShootersCountUpdated;
    }

    private void ShootersCountUpdated(uint shootersCounter)
    {
        SetScore(shootersCounter);
    }

    private void GameplayStarted()
    {
        SetScore(LevelSettingsManager.Instance.CurrentLevel.ShootersCount);
    }

    private void SetScore(uint shootersCount)
    {
        shooterCounter.text = shootersCount.ToString();
    }

    private void OnGoToMainMenuClick()
    {
        GameplayManager.Instance.SetEndGameplayState();
    }
}

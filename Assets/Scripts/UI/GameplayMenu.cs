using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameplayMenu : BaseMenu
{
    [SerializeField]
    private TextMeshProUGUI objectsCounter;
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
    }

    private void GameplayStarted()
    {
        objectsCounter.text = LevelSettingsManager.Instance.CurrentLevel.ObjectsCount.ToString();
    }

    private void OnGoToMainMenuClick()
    {
        GameplayManager.Instance.SetEndGameplayState();
    }

    private void UnsubscribeFromEvents()
    {
        EventsManager.Instance.GameplayStarted -= GameplayStarted;
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}

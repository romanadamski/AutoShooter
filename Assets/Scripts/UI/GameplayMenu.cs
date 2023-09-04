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
        EventsManager.Instance.PlayerLoseLife += OnPlayerLoseLife;
        EventsManager.Instance.PlayerSpawned += OnPlayerSpawned;
    }

    private void OnGoToMainMenuClick()
    {
        GameplayManager.Instance.SetEndGameplayState();
    }

    private void OnPlayerSpawned(uint lives)
    {
        SetLivesCounter(lives);
    }

    private void OnPlayerLoseLife(uint lives)
    {
        SetLivesCounter(lives);
    }

    private void SetLivesCounter(uint lives)
    {
        objectsCounter.text = lives.ToString();
    }

    private void UnsubscribeFromEvents()
    {
        if (!EventsManager.Instance) return;

        EventsManager.Instance.PlayerLoseLife -= OnPlayerLoseLife;
        EventsManager.Instance.PlayerSpawned -= OnPlayerSpawned;
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}

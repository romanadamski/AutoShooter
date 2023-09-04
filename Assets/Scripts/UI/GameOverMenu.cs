using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : BaseMenu
{
    [SerializeField]
    private Button goToMainMenuButton;

    private void Awake()
    {
        goToMainMenuButton.onClick.AddListener(OnGoToMainMenuClick);
    }

    private void OnGoToMainMenuClick()
    {
        GameplayManager.Instance.SetEndGameplayState();
    }
}

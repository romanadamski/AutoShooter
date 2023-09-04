using UnityEngine;
using UnityEngine.UI;

public class MainMenu : BaseMenu
{
    [SerializeField]
    private StartLevelButton startLevelButton;
    [SerializeField]
    private Transform levelButtonParent;
    [SerializeField]
    private LevelButton levelButtonPrefab;

    private LevelButton _activeLevelButton;

    private void Start()
    {
        startLevelButton.Init(OnPlayClick);

        foreach (var level in LevelSettingsManager.Instance.LevelSettings)
        {
            var levelButton = Instantiate(levelButtonPrefab, levelButtonParent);
            levelButton.Init(level.ObjectsCount, level.ImageColor, () => OnLevelButtonClick(levelButton, level.ObjectsCount));
        }
    }

    public override void Show()
    {
        base.Show();

        if (_activeLevelButton)
        {
            _activeLevelButton?.DeselectButton();
            _activeLevelButton = null;
        }
        startLevelButton.ToggleButton(false);
    }

    private void OnLevelButtonClick(LevelButton button, uint objectsCount)
    {
        if (_activeLevelButton)
        {
            _activeLevelButton.DeselectButton();
        }

        _activeLevelButton = button;
        _activeLevelButton.SelectButton();

        LevelSettingsManager.Instance.SetLevelNumber(objectsCount);
        startLevelButton.ToggleButton(true);
    }

    private void OnPlayClick()
    {
        GameManager.Instance.SetLevelState();
    }
}

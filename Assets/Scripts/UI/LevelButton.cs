using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI objectsCountText;
    [SerializeField]
    private Image buttonImage;
    [SerializeField]
    private Sprite selectedButtonSprite;

    private Button _button;
    private Sprite _normalButtonSprite;
    private RectTransform _rectTransform;
    private float _selectedButtonScale = 1.1f;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _normalButtonSprite = _button.image.sprite;
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Init(uint objectsCount, Color imageColor, UnityEngine.Events.UnityAction unityAction)
    {
        objectsCountText.text = objectsCount.ToString();
        buttonImage.color = imageColor;
        _button.onClick.AddListener(() => unityAction?.Invoke());
    }

    public void SelectButton()
    {
        _button.image.sprite = selectedButtonSprite;
        _rectTransform.localScale *= _selectedButtonScale;
    }

    public void DeselectButton()
    {
        _button.image.sprite = _normalButtonSprite;
        _rectTransform.localScale /= _selectedButtonScale;
    }
}

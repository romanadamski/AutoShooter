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

    private Button button;
    private Sprite _normalButtonSprite;
    private RectTransform _rectTransform;
    private float _selectedButtonScale = 1.1f;

    private void Awake()
    {
        button = GetComponent<Button>();
        _normalButtonSprite = button.image.sprite;
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Init(uint objectsCount, Color imageColor, UnityEngine.Events.UnityAction unityAction)
    {
        objectsCountText.text = objectsCount.ToString();
        buttonImage.color = imageColor;
        button.onClick.AddListener(() => unityAction?.Invoke());
    }

    public void SelectButton()
    {
        button.image.sprite = selectedButtonSprite;
        _rectTransform.localScale *= _selectedButtonScale;
    }

    public void DeselectButton()
    {
        button.image.sprite = _normalButtonSprite;
        _rectTransform.localScale /= _selectedButtonScale;
    }
}

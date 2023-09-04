using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartLevelButton : MonoBehaviour
{
    [SerializeField]
    private Image image;

    private Button _button;

    public void Init(UnityEngine.Events.UnityAction unityAction)
    {
        _button.onClick.AddListener(unityAction);
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void ToggleButton(bool toggle)
    {
        _button.interactable = toggle;
        image.gameObject.SetActive(toggle);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    public Button button;
    public Text text;

    [HideInInspector]
    public bool clicked;

    private void Awake()
    {
        button.onClick.AddListener(() => OnClicked());
    }

    private void OnClicked()
    {
        clicked = true;
    }
}

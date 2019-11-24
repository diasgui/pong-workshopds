using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonView : MonoBehaviour
{
    [SerializeField] Text _buttonLabel;
    Action _buttonCallback;

    public void Setup(string text, Action callback)
    {
        _buttonLabel.text = text;
        _buttonCallback = callback;
    }

    public void OnClick()
    {
        _buttonCallback?.Invoke();
    }
}

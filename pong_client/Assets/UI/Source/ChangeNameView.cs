using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNameView : View
{
    [SerializeField] InputField _nameInput;
    Action<string> _onConfirm;
    Action _onCancel;

    public void Setup(Action<string> onConfirm, Action onCancel)
    {
        _onConfirm = onConfirm;
        _onCancel = onCancel;
    }
    
    public void OnConfirm()
    {
        _onConfirm?.Invoke(_nameInput.text);
    }

    public void OnCancel()
    {
        _onCancel?.Invoke();
    }
}

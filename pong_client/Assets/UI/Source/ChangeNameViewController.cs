using System;

public class ChangeNameViewController : ViewController<ChangeNameView>
{
    private readonly PlayerClient _playerClient;
    private Action _closeCallback;
    
    public ChangeNameViewController(ChangeNameView view, PlayerClient playerClient) : base(view)
    {
        _playerClient = playerClient;
    }

    public void Setup(Action closeCallback)
    {
        _closeCallback = closeCallback;
        View.Setup(ChangeName, closeCallback);
    }

    void ChangeName(string newName)
    {
        if(newName.Length > 2) _playerClient.ChangeName(newName);
        _closeCallback.Invoke();
    }
}

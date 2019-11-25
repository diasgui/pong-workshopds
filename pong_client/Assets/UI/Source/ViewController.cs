

public abstract class ViewController<T> where T:View
{
    private readonly T _view;
    
    protected ViewController(T view)
    {
        _view = view;
        _view.OnDestroyCallback = Dismiss;
    }

    public virtual void Dismiss()
    {
//        _view.gameObject.SetActive(false);
        // TODO: Destroy and clear...
    }

    public T View => _view;
}

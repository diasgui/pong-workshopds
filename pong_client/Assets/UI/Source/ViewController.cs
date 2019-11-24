

public abstract class ViewController<T> where T:View
{
    private readonly T _view;
    
    protected ViewController(T view)
    {
        _view = view;
        
//        _view.gameObject.SetActive(false);
    }

    public void Present()
    {
//        _view.gameObject.SetActive(true);
    }

    public void Dispose()
    {
//        _view.gameObject.SetActive(false);
        // TODO: Destroy and clear...
    }

    public T View => _view;
}

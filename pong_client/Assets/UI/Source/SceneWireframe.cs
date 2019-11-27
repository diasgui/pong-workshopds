using UnityEngine;

public class SceneWireframe : MonoBehaviour
{
    [SerializeField] Transform _uiHolder;

    public void PresentViewController<T>(ViewController<T> vc) where T : View
    {
        foreach (Transform child in _uiHolder)
        {
            Destroy(child.gameObject);
        }
        
        vc.View.transform.SetParent(_uiHolder, false);
    }
    
    public void PresentView(View view)
    {
        foreach (Transform child in _uiHolder)
        {
            Destroy(child.gameObject);
        }
        
        view.transform.SetParent(_uiHolder, false);
    }
}

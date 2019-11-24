using System.IO;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
    private AssetBundle _viewBundle;
    
    private void Awake()
    {
        _viewBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "viewbundle"));
    }

    public T LoadView<T>(string viewName) where T:View
    {
        var prefab = _viewBundle.LoadAsset<GameObject>(viewName);
        return Instantiate(prefab).GetComponent<T>();
    }
}

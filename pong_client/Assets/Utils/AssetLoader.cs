using System.IO;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
    public T LoadView<T>(string viewName) where T:View
    {
        var viewBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "viewbundle"));
        var prefab = viewBundle.LoadAsset<GameObject>(viewName);
        return Instantiate(prefab).GetComponent<T>();
    }
}

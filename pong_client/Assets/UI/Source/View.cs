using System;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    public Action OnDestroyCallback;
    
    private void OnDestroy()
    {
        OnDestroyCallback?.Invoke();
    }
}

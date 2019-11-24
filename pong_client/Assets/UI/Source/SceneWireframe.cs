﻿using UnityEngine;

public class SceneWireframe : MonoBehaviour
{
    [SerializeField] Transform _uiHolder;

    public void PresentView(View view)
    {
        foreach (Transform child in _uiHolder)
        {
            Destroy(child.gameObject);
        }
        
        view.transform.SetParent(_uiHolder, true);
    }
}
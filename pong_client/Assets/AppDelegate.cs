﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AppDelegate : MonoBehaviour
{
    [SerializeField] ClientRequester _clientRequester;
    [SerializeField] SceneWireframe _wireframe;
    [SerializeField] AssetLoader _assetLoader;
    [SerializeField] PongNetworkManager _networkManager;
    
    void Start()
    {
        InitializeGame();
    }

    void Restart()
    {
        //SceneManager.LoadScene(0);
        // Do some cleanup
        //InitializeGame();
    }

    void InitializeGame()
    {
        _clientRequester.Clear();
        _clientRequester.RequestFailCallback += Restart;
        
        new GameInitializer(_clientRequester, _wireframe, _assetLoader, _networkManager)
            .Initialize();
    }
    
}

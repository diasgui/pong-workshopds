using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppDelegate : MonoBehaviour
{
    [SerializeField] ClientRequester _clientRequester;
    
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
        
        new GameInitializer(_clientRequester)// We may add inject some dependencies on this constructor
            .Initialize();
    }
    
}

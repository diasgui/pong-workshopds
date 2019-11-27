using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

public static class ServerDefines
{
#if UNITY_EDITOR
    public static readonly string BaseURL = "http://localhost:4000/api/";
#else
    public static readonly string BaseURL = "http://192.168.0.14:4000/api/";
#endif
}

public class ClientRequester : MonoBehaviour
{
    [HideInInspector] public string Token;
    public event Action RequestFailCallback;

    public void Clear()
    {
        RequestFailCallback = null;
    }

    public void RequestGet(string route, Action<JSONNode> success, Action fail = null)
    {
        StartCoroutine(SendGet(route, success, fail));
    }
    
    public void Request(string route, Dictionary<string, string> parameters, Action<JSONObject> success, Action fail = null)
    {
        WWWForm form = new WWWForm();
        foreach (var parameter in parameters)
        {
            form.AddField(parameter.Key, parameter.Value);
        }
        
        StartCoroutine(SendPost(route, form, form.headers, success, fail));
    }
    
    public void RequestAuth(string route, Dictionary<string, string> parameters, Action<JSONObject> success, Action fail = null)
    {
        WWWForm form = new WWWForm();
        foreach (var parameter in parameters)
        {
            form.AddField(parameter.Key, parameter.Value);
        }
        
        Dictionary<string, string> headers = form.headers;
        headers["Authorization"] = "Bearer " + Token;
        
        StartCoroutine(SendPost(route, form, headers, success, fail));
    }

    IEnumerator SendPost(string route, WWWForm form, Dictionary<string, string> headers, Action<JSONObject> success, Action fail)
    {
        Debug.Log($"POST: {ServerDefines.BaseURL + route}");
        using (WWW www = new WWW(ServerDefines.BaseURL + route, form.data, headers))
        {
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                JSONObject response = JSON.Parse(www.text)["data"].AsObject;
                if (response["token"] != null) Token = response["token"];
                
                Debug.Log($"{www.text}");
                success?.Invoke(response);
            }
            else
            {
                Debug.LogError($"Error: {www.error}");
                fail?.Invoke();
            }
        }
    }

    IEnumerator SendGet(string route, Action<JSONNode> success, Action fail)
    {
        Debug.Log($"GET: {ServerDefines.BaseURL + route}");
        using (UnityWebRequest www = UnityWebRequest.Get(ServerDefines.BaseURL + route))
        {
            yield return www.SendWebRequest();
            if(www.isNetworkError || www.isHttpError) {
                Debug.LogError($"Error: {www.error}");
                fail?.Invoke();
            }
            else
            {
                JSONNode response = JSON.Parse(www.downloadHandler.text)["data"];
                
                Debug.Log($"{www.downloadHandler.text}");
                success?.Invoke(response);
            }
        }
    }
}

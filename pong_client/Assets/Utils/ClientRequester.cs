using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClientRequester : MonoBehaviour
{
    public event Action RequestFailCallback;
    string _token;

    public void Clear()
    {
        RequestFailCallback = null;
    }
    
    public void Request(string route, Dictionary<string, string> parameters, Action<Dictionary<string, string>> success, Action fail = null)
    {
        WWWForm form = new WWWForm();
        foreach (var parameter in parameters)
        {
            form.AddField(parameter.Key, parameter.Value);
        }
        form.headers["Content-Type"] = "application/json";
        
        StartCoroutine(Post(route, form, success, fail));
    }
    
    public void RequestAuth(string route, Dictionary<string, string> parameters, Action<Dictionary<string, string>> success, Action fail = null)
    {
        WWWForm form = new WWWForm();
        foreach (var parameter in parameters)
        {
            form.AddField(parameter.Key, parameter.Value);
        }
        form.headers["Authorization"] = "Bearer " + _token;
        form.headers["Content-Type"] = "application/json";
        
        StartCoroutine(Post(route, form, success, fail));
    }

    IEnumerator Post(string route, WWWForm form, Action<Dictionary<string, string>> success, Action fail)
    {
        using (UnityWebRequest www = UnityWebRequest.Post("http://" + route, form))// TODO: Fix route with full URL
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError($"{www.responseCode}: {www.GetResponseHeaders()}");
                if (fail != null) fail.Invoke();
                else RequestFailCallback?.Invoke();
            }
            else
            {
                success?.Invoke(www.GetResponseHeaders());
                www.GetResponseHeaders().TryGetValue("auth_token", out _token);
            }
        }
    }
}

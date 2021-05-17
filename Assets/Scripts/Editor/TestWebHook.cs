using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Utils;

#if UNITY_EDITOR
using UnityEditor;

public class TestWebHook : MonoSingleton<TestWebHook>
{


    private const string WebHookURL = "https://discord.com/api/webhooks/843854440494137376/OQEpL1xA4gWXOwJPpdSnwPdntc7P1kgP9z6GEH07hGg5bWf8IDum0hlK7x9NiJp79EAD";

    [MenuItem("Test/SendWebHook")]
    public static void SendWebHook()
    {
        SendWebHookInternal("Test message form unity project ", (isSuccess, msg) =>
        {
            if (!isSuccess)
            {
                Debug.LogError("Send Fail. Error with  :" + msg);
                return;
            }

            Debug.Log("Sent successfully");
        });
    }

    [MenuItem("Test/BakeLightMap And Call")]
    public static void BakeAndCall()
    {
        var result = Lightmapping.Bake();

        SendWebHookInternal($"Lightmapping.Bake Complete. result is {result}", (isSuccess, msg) =>
        {
            if (!isSuccess)
            {
                Debug.LogError("Send Fail. Error with  :" + msg);
                return;
            }

            Debug.Log("Sent successfully");
        });
    }

    static void SendWebHookInternal(string message, Action<bool, string> OnComplete)
    {
        WWWForm form = new WWWForm();
        form.AddField("content", message);
        using (var www = UnityWebRequest.Post(WebHookURL, form))
        {
            www.SendWebRequest();
            while (!www.isDone) ;

            if (www.result != UnityWebRequest.Result.Success)
            {
                OnComplete?.Invoke(false, www.error);
            }
            else
            {
                OnComplete?.Invoke(true, string.Empty);
            }
        }
    }

}



#else

public class TestWebHook : MonoBehaviour{}

#endif
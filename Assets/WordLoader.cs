using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
public class WordLoader : MonoBehaviour
{
    private string uri = "https://random-word-api.vercel.app/api?words=10";
    private string[] _tempContainer = new string[10];
    
    public void GetWords(Action<string[]> onComplete)
    {
        StartCoroutine(FetchWords(onComplete));
    }
    
    private IEnumerator FetchWords(Action<string[]> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + webRequest.error);
                    callback?.Invoke(null); // Return null on error
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + webRequest.error);
                    callback?.Invoke(null);
                    break;
                case UnityWebRequest.Result.Success:
                    string[] words = ParseWordsFromJson(webRequest.downloadHandler.text);
                    callback?.Invoke(words); // Return the words array
                    break;
            }
        }
    }
    
    private string[] ParseWordsFromJson(string jsonResponse)
    {
        string cleanedResponse = jsonResponse.Trim('[', ']');
        string[] words = cleanedResponse.Split(',');
        
        for (int i = 0; i < words.Length; i++)
        {
            words[i] = words[i].Trim().Trim('"');
        }
        
        return words;
    }
}

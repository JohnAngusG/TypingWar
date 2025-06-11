using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject sentencePrefab;
    [SerializeField] private Canvas canvas;
    
    void Start()
    {
        StartCoroutine(GetRequest("https://random-word-api.vercel.app/api?words=1&length=9"));
    }

    void Awake()
    {
        Messenger<int>.AddListener(GameEvent.WORD_DONE, OnWordDone);
    }

    void onDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.WORD_DONE, OnWordDone);
    }

    void OnWordDone(int attempts)
    {
        print(attempts);
        StartCoroutine(GetRequest("https://random-word-api.vercel.app/api?words=1&length=9"));
    }

    void CreatePrefab(string words)
    {
        GameObject sentence = Instantiate(sentencePrefab, canvas.transform); 
        Word word = sentence.GetComponent<Word>();
        word.characters = words;
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text.Substring(2, webRequest.downloadHandler.text.Length - 4));
                    CreatePrefab(webRequest.downloadHandler.text.Substring(2, webRequest.downloadHandler.text.Length - 4));
                    
                    break;
            }
        }
    }
}

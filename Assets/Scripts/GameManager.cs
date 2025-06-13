using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject sentencePrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private WordLoader wordLoader;

    private string[] _playerOneWords;

    private void Start()
    {
        wordLoader.GetWords(OnWordsReceived);
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
    }

    
    private void OnWordsReceived(string[] words)
    {
        if (words != null)
        {
            _playerOneWords = words;
        }
        else
        {
            Debug.LogError("Failed to receive words");
        }
    }
    
    void CreatePrefab(string words)
    {
        GameObject sentence = Instantiate(sentencePrefab, canvas.transform); 
        Word word = sentence.GetComponent<Word>();
        word.characters = words;
    }

}
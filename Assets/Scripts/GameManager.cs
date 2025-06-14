using System;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject sentencePrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private WordLoader wordLoader;
    
    private Word _currentWordObject;
    private string[] _playerOneWords;
    private int _currentWord; 
    
    private void Start()
    {
        wordLoader.GetWords(OnWordsReceived);
    }


    void Awake()
    {
        Messenger.AddListener(GameEvent.WORD_DONE, OnWordDone);
        Messenger<char>.AddListener(GameEvent.KEY_PRESSED, OnKeyPressed);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.WORD_DONE, OnWordDone);
        Messenger<char>.RemoveListener(GameEvent.KEY_PRESSED, OnKeyPressed);
    }

    void OnWordDone()
    {
        _currentWord++;
        if (_currentWord == _playerOneWords.Length)
        {
            return;

        }
        
        CreatePrefab(_playerOneWords[_currentWord]);
    }

    
    private void OnWordsReceived(string[] words)
    {
        if (words != null)
        {
            _playerOneWords = words;
            CreatePrefab(_playerOneWords[_currentWord]);
        }
        else
        {
            Debug.LogError("Failed to receive words");
        }
    }

    private void OnKeyPressed(char key)
    {
        print(key);
        if (key == Char.ToUpper(_currentWordObject.GetCurrentChar()))
        {
            _currentWordObject.IncrementIndex();
        }
    }
    
    void CreatePrefab(string characters)
    {
        GameObject sentence = Instantiate(sentencePrefab, canvas.transform); 
        Word word = sentence.GetComponent<Word>();
        word.characters = characters;
        _currentWordObject = word;
    }

}
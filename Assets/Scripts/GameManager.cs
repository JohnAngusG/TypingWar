using System;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject roundPrefab;
    [SerializeField] private WordLoader wordLoader;

    private string[] _playerOneWords;
    private int _currentWord; 
    
    private void Start()
    {
        wordLoader.GetWords(OnWordsReceived);
    }
    
    void Awake()
    {
        Messenger.AddListener(GameEvent.WORD_DONE, OnWordDone);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.WORD_DONE, OnWordDone);
    }

    void OnWordDone()
    {
        _currentWord++;
        if (_currentWord == _playerOneWords.Length)
        {
            return;

        }

        if (_currentWord == 1)
        {
            ReverseNextWord();
        }
        
        CreateRound(_playerOneWords[_currentWord]);
    }

    private void ReverseNextWord()
    {
        if (_currentWord < _playerOneWords.Length - 2)
        {
            char[] tempContainer = _playerOneWords[_currentWord + 1].ToCharArray();
            Array.Reverse(tempContainer);
            _playerOneWords[_currentWord + 1] =  new string(tempContainer);
        }
        
    }
    
    
    private void OnWordsReceived(string[] words)
    {
        if (words != null)
        {
            _playerOneWords = words;
            CreateRound(_playerOneWords[_currentWord]);
        }
        else
        {
            Debug.LogError("Failed to receive words");
        }
    }
    
    void CreateRound(string characters)
    {
        GameObject roundObj = Instantiate(roundPrefab); 
        Round round = roundObj.GetComponent<Round>();
        round.StartRound(characters);
    }
}
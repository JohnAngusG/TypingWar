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

        if (_currentWord == 3)
        {
            wordLoader.GetForeignWord(OnForeignWordReceived);
        }

        if (_currentWord == 5)
        {
            PigLatinNextWord();
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

    private void PigLatinNextWord()
    {
        string tempWord = _playerOneWords[_currentWord + 1];
        
        if (tempWord[0] == 'a' || tempWord[0] == 'e' || tempWord[0] == 'i' || tempWord[0] == 'o' || tempWord[0] == 'u')
        {
            tempWord = tempWord + "way";
        }
        else
        {
            tempWord = tempWord.Substring(1) + tempWord[0] + "ay";
        }
        _playerOneWords[_currentWord + 1] = tempWord;
    }
    
    
    
    private void OnForeignWordReceived(string word)
    {
        print(word);
        if (_currentWord < _playerOneWords.Length - 2)
        {
            if (word != null)
            {
                _playerOneWords[_currentWord + 1] = word;
            }
        }
        
    }
    
    void CreateRound(string characters)
    {
        GameObject roundObj = Instantiate(roundPrefab); 
        Round round = roundObj.GetComponent<Round>();
        round.StartRound(characters);
    }
}
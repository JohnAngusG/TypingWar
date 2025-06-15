using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Round : MonoBehaviour
{
    private List<MyChar> _word = new List<MyChar>();
    private int _index;
    private Canvas _canvas;
    [SerializeField] private GameObject normalWord;
    private GameObject _wordObj; 
    private TextMeshProUGUI _textBox;
    
    void Awake()
    {
        Messenger<char>.AddListener(GameEvent.KEY_PRESSED, OnKeyPressed);
        Messenger.AddListener(GameEvent.BACKSPACE_PRESSED, OnDeletePressed);
    }

    void OnDestroy()
    {
        Messenger<char>.RemoveListener(GameEvent.KEY_PRESSED, OnKeyPressed);
        Messenger.RemoveListener(GameEvent.BACKSPACE_PRESSED, OnDeletePressed);
    }

    public void StartRound(string word)
    {
        _canvas = GameObject.FindAnyObjectByType<Canvas>();
        
        SetChars(word);
        _wordObj = Instantiate(normalWord, _canvas.transform);
        _textBox = _wordObj.GetComponent<TextMeshProUGUI>();
        _textBox.text = GetCurrentWord();
    }
    
    private string GetCurrentWord()
    {
        string formattedWord = "";
        for (int i = 0; i < _word.Count; i++)
        {
            if (!_word[i].GetTyped())
            {
                formattedWord += _word[i].GetChar();
            }
            else
            {
                formattedWord += $"<color={(_word[i].GetCorrect() ? "green" : "red")}>{_word[i].GetChar()}</color>";
            }
        }
        return formattedWord;
    }

    private void UpdateWord()
    {
        _textBox.text = GetCurrentWord();

        bool allCorrect = true;
        bool allTyped = true; 
        
        for (int i = 0; i < _word.Count; i++)
        {
            allCorrect = allCorrect && _word[i].GetCorrect();
            allTyped = allTyped && _word[i].GetTyped();
        }

        if (allCorrect && allTyped)
        {
            Messenger.Broadcast(GameEvent.WORD_DONE);
            Destroy(_wordObj);
            Destroy(this.gameObject);
        }
    }
    
    private void SetChars(string word)
    {
        for(int i = 0; i < word.Length; i++)
        {
            _word.Add(new MyChar(word[i]));
        }
    }
    
    private void OnKeyPressed(char key)
    {   
        if (_index >= _word.Count)
        {
            _index = _word.Count; 
            
            return;
        }
        
        _word[_index].SetCorrect(key == (_word[_index].GetChar()));
        _word[_index].SetTyped(true);
        UpdateWord();
        _index++;
    }

    private void OnDeletePressed()
    {
        if (_index <= 0)
        {
            _index = 0;
            return;
        }
        
        _index--;
        _word[_index].SetCorrect(false);
        _word[_index].SetTyped(false);
        UpdateWord();
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Round : MonoBehaviour
{
    private List<MyChar> _word;
    private int _index;
    
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

    public string GetCurrentWord()
    {
        string formattedWord = "";
        for (int i = 0; i < _word.Count; i++)
        {
            if (!_word[i].GetTyped())
            {
                formattedWord += _word[i].GetChar();
            }
            formattedWord += $"<color={(_word[i].GetCorrect() ? "green" : "red")}>{_word[i].GetChar()}</color>";
        }
        return formattedWord;
    }
    
    
    public void SetChars(string word)
    {
        for (int i = 0; i < word.Length; i++)
        {
            _word.Add(new MyChar(word[i]));
        }
    }
    
    
    
    
    private void OnKeyPressed(char key)
    {
        _word[_index].SetCorrect(key == _word[_index].GetChar());
        _word[_index].SetTyped(true);
        _index++;
    }

    private void OnDeletePressed()
    {
        print("Delete pressed!");
        _word[_index].SetCorrect(false);
        _word[_index].SetTyped(false);
        _index--;
    }
}

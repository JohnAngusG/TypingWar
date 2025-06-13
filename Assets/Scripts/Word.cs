using System;
using TMPro;
using UnityEngine;

public class Word : MonoBehaviour
{
    [SerializeField] public string characters;
    [SerializeField] private TextMeshProUGUI textBox;
    private int _index = 0;
    private int _attempts = 0;
    void Start()
    {
        textBox.text = characters;
    }

    void IncrementIndex()
    {
        _index++;
        textBox.text = characters.Substring(_index, characters.Length - _index);
    }
    
}
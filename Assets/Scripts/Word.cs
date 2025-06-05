using System;
using TMPro;
using UnityEngine;

public class Word : MonoBehaviour
{
    [SerializeField] public string characters;
    [SerializeField] private TextMeshProUGUI textBox;
    private int _index = 0;
    
    void Start()
    {
        textBox.text = characters;
    }

    void IncrementIndex()
    {
        _index++;
        textBox.text = characters.Substring(_index, characters.Length - _index);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    if (keyCode == KeyCode.Space && characters[_index] == ' ')
                    {
                        IncrementIndex();
                    }
                    else if (Char.ToUpper(characters[_index]) == keyCode.ToString()[0])
                    {
                        IncrementIndex();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        if (_index == characters.Length)
        {
            Destroy(this.gameObject);
        }
    }
}

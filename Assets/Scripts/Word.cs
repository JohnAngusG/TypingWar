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
    public char GetCurrentChar()
    {
        return characters[_index];
    }
    public void IncrementIndex()
    {
        _index++;
        textBox.text = characters.Substring(_index, characters.Length - _index);

        if (_index == characters.Length)
        {
            Messenger.Broadcast(GameEvent.WORD_DONE);
            Destroy(this.gameObject);
        }
    }
    
    
    
}
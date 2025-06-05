using TMPro;
using UnityEngine;

public class Wordle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI guess;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    if (keyCode >= KeyCode.A && keyCode <= KeyCode.Z)
                    {
                        guess.text += keyCode.ToString();
                        
                    }
                    else if (keyCode == KeyCode.Backspace && guess.text.Length > 0)
                    {
                        guess.text = guess.text.Substring(0, guess.text.Length - 1);
                    }
                    
                }
            }
        }
    }

    public string GetGuess()
    {
        return guess.text;
    }
    
}

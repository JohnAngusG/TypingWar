using TMPro;
using UnityEngine;

public class Word : MonoBehaviour
{
    [SerializeField] private string characters;
    [SerializeField] private TextMeshProUGUI textBox; 
    void Start()
    {
        textBox.text = characters;
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
                    Debug.Log("Key pressed: " + keyCode);
                    break;
                }
            }
        }
    }
}

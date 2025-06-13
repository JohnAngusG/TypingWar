using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    Messenger<char>.Broadcast(GameEvent.KEY_PRESSED, keyCode.ToString()[0]);
                }
            }
        }
        
    }
}

using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // TODO: Handle backspace somehow as only the first character is being sent. 
    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    if (keyCode == KeyCode.Backspace)
                    {
                        Messenger.Broadcast(GameEvent.BACKSPACE_PRESSED);
                    }
                    else if (keyCode >= KeyCode.A && keyCode <= KeyCode.Z)
                    {
                        Messenger<char>.Broadcast(GameEvent.KEY_PRESSED, Char.ToLower(keyCode.ToString()[0]));
                    }
                    else
                    {
                        return;
                    }

                }
            }
        }
        
    }
}

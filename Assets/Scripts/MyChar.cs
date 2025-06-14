public class MyChar
{
    private char _character;
    private bool _typed = false;
    private bool _correct = false;

    public MyChar(char character)
    {
        _character = character;
    }

    public bool GetTyped()
    {
        return _typed;
    }

    public bool GetCorrect()
    {
        return _correct;
    }
    
    public void SetTyped(bool typed)
    {
        _typed = typed;
    }

    public void SetCorrect(bool correct)
    {
        _correct = correct;
    }

    public char GetChar()
    {
        return _character;
    }
}

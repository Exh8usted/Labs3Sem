namespace VectorTest;

// Для описания исключений при вводе
public class CustomExceptions : Exception
{
    public string CustomMessage { get; }
    public CustomExceptions(string CustomMessage)
    {
        this.CustomMessage = CustomMessage;
    }
    // public CustomExceptions(string Message) : base (Message) { }
}

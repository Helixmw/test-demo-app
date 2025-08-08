namespace GolfingDataAccessLib.Exceptions;

public class ObjectNotFoundException : Exception
{
    public string Caption { get; private set; }
    public ObjectNotFoundException(string message,  string? caption = null):base(message)
    {
         Caption = caption ?? String.Empty;
    }
}
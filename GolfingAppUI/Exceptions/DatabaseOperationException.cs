namespace GolfingAppUI.Exceptions;


public class DatabaseOperationException : Exception
{
    public DatabaseOperationException(string message):base(message)
    {
        
    }
}
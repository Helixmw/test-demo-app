namespace GolfingDataAccessLib.Exceptions;

public class ScheduleOperationException : Exception
{
    public string Caption { get; private set; }
    public ScheduleOperationException(string message, string? caption = null) : base(message)
    {
        Caption = caption ?? String.Empty;
    }
}
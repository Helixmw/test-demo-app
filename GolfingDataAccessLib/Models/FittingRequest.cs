namespace GolfingDataAccessLib.Models;
/// <summary>
/// This model is for the administrator viewing fitting 
/// requests from users
/// </summary>
public class FittingRequest : BaseSchedule
{
    public Guid ScheduleId { get; set; } 
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Comment { get; set; }

    public bool IsAcknowledged { get; set; }
}
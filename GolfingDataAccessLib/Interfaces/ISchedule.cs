namespace GolfingDataAccessLib.interfaces;
/// <summary>
/// Properties to be implemented in each service type
/// </summary>
public interface ISchedule{
    public string UserId { get; set; } 
    public DateTime AvailableDate { get; set; }
    public string AvailableTime {get; set;}
    public string Comment { get; set;}
}
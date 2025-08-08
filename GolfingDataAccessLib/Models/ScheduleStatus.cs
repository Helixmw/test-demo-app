namespace GolfingDataAccessLib.Models;
/// <summary>
/// 'ScheduleStatus' is a model only for the table join
/// </summary>
public class ScheduleStatus{
    public string ScheduleId { get; set; } 
    public DateTime AvaliableDate { get; set; }
    public string AvailableTime {get; set;}
    public string Comment { get; set;}
    public string Status {get; set; }
}
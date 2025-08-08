namespace GolfingDataAccessLib.Models;

//Base schedule properties for Swing Analysis and Fitting
public class BaseSchedule: MetaData{
    public DateTime AvailableDate { get; set; }
    public string AvailableTime {get; set;}
    public string? Comment { get; set;}
}
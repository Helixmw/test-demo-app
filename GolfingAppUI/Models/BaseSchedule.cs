using System.ComponentModel.DataAnnotations;

namespace GolfingAppUI.Models;

public class BaseSchedule{
    [Required(ErrorMessage = "Please provide date for your schedule.")]
    public DateTime AvailableDate { get; set; }

    [Required(ErrorMessage = "Please provide date a time of day.")]
    public TimeOnly AvailableTime {get; set;}


    public String? Comment { get; set;}
}
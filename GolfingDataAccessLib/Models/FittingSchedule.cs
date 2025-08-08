using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GolfingDataAccessLib.Data;
using GolfingDataAccessLib.Enums;
using GolfingDataAccessLib.interfaces;

namespace GolfingDataAccessLib.Models;

public class FittingSchedule : BaseSchedule, ISchedule{

    [Key]
    public Guid ScheduleId { get; set; } 
    public FittingStatus FittingStatus { get; set; }

    [ForeignKey("UserId")]
    public ApplicationUser ApplicationUser { get; set; }

    public bool IsAcknowledged {get; set;} = false;

    public string Status {get; set; } = ProgressStatus.Submitted.ToString();

}
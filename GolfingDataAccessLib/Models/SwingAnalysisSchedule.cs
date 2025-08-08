using System.ComponentModel.DataAnnotations;
using GolfingDataAccessLib.Enums;
using GolfingDataAccessLib.interfaces;

namespace GolfingDataAccessLib.Models;

public class SwingAnalysisSchedule : BaseSchedule, ISchedule{
   
    [Key]
    public Guid ScheduleId { get; set; } 

    public string Status { get; set; } = ProgressStatus.Submitted.ToString();
}
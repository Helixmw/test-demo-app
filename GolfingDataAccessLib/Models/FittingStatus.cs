using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfingDataAccessLib.Models;

public class FittingStatus{

    [Key]
    public Guid StatusId { get; set; }

    public Guid ScheduleId { get; set; }  

    public string Status {get; set; }

    [ForeignKey("ScheduleId")]
    public FittingSchedule FittingSchedule { get; set; }


}
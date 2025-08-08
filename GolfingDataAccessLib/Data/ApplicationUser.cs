using GolfingDataAccessLib.Models;
using Microsoft.AspNetCore.Identity;

namespace GolfingDataAccessLib.Data;

public class ApplicationUser : IdentityUser{

    public string? Address { get; set;}
    public int GolfClubSize { get; set;}
    public ICollection<FittingSchedule>? FittingSchedules{ get; set; }
}
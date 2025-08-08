using GolfingDataAccessLib.DTOs.User;

namespace GolfingDataAccessLib.DTOs.Fitting;

public class GetFittingDTO : IUserDTO
{
    public string Email { get; set; }
    public DateTime AvailableDate { get; set; }
    public TimeOnly AvailableTime { get; set; }
    public string Comment { get; set; }
    public string Role { get; set; }
}
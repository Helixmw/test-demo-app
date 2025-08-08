namespace GolfingDataAccessLib.DTOs.User;

public class GetUserDTO : IUserDTO
{
    public string? UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}
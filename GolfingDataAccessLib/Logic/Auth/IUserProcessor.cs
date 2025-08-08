using System.Security.Claims;
using GolfingDataAccessLib.DTOs.User;


namespace GolfingDataAccessLib.Logic.Auth;

public interface IUserProcessor{

    Task SignUpAsync(SignUpDTO signUpDTO);
    Task<GetUserDTO> LoginAsync(LoginDTO loginDTO, string authenticationType);
    Task LogoutAsync();   

}
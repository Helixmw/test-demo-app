using System.Security.Claims;
using GolfingDataAccessLib.Data;
using GolfingDataAccessLib.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace GolfingDataAccessLib.Services.Auth;

public interface IUserService{

    Task SignUpAsync(SignUpDTO signUpDTO);
    Task<GetUserDTO> LoginAsync(LoginDTO loginDTO, string authenticationType);
    Task LogoutAsync();   

}
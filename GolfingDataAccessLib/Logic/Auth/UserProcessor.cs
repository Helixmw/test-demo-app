using System.Net;
using System.Security.Claims;
using GolfingDataAccessLib.DTOs.User;
using GolfingDataAccessLib.Exceptions;
using GolfingDataAccessLib.Services.Auth;


namespace GolfingDataAccessLib.Logic.Auth;
/// <summary>
/// User Auth Processor
/// </summary>
public class UserProcessor : IUserProcessor
{
    private readonly IUserService _userService;
    public UserProcessor(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<GetUserDTO> LoginAsync(LoginDTO loginDTO, string authenticationType)
    {
        var user = await _userService.LoginAsync(loginDTO, authenticationType);
        return user;
    }


    public async Task SignUpAsync(SignUpDTO signUpDTO)
    {
        await _userService.SignUpAsync(signUpDTO);
    }

      public async Task LogoutAsync()
    {
        try{
            await _userService.LogoutAsync();
        }catch(Exception){
            throw new AuthorizationException("Unable to log out. Try again later", "Log Out Problem");
        }
    }
}
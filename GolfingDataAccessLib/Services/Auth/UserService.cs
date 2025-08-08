using System.Security.Claims;
using GolfingDataAccessLib.Data;
using GolfingDataAccessLib.DTOs.User;
using Microsoft.AspNetCore.Identity;
using GolfingDataAccessLib.Services.Auth;
using GolfingDataAccessLib.Exceptions;
using GolfingDataAccessLib.Enums;

namespace GolfingDataAccessLib.Services.Auth;
/// <summary>
/// Derived UserService class from AuthBaseService with user authentication methods
/// </summary>
public class UserService : AuthBaseService, IUserService
{

    public UserService(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager
        ):base(signInManager,userManager,roleManager)
    {
        
    }
    //Sign Up
    public async Task SignUpAsync(SignUpDTO signUpDTO)
    {
       
        await FindUserByEmailOnSignUpAsync(signUpDTO.Email);

            var user = new ApplicationUser
            {
                UserName = signUpDTO.UserName.Replace(" ",""),
                Email = signUpDTO.Email,
            };

        var result =  await _userManager.CreateAsync(user, signUpDTO.Password);
            if (!result.Succeeded)        
            throw new AuthorizationException("Unable to Sign Up. Try again later","Sign Up Problem"); 
            
        //Default role
        await _userManager.AddToRoleAsync(user, Roles.Customer.ToString()); 
    }

    //Login
   public async Task<GetUserDTO> LoginAsync(LoginDTO loginDTO, string authenticationType)
{
    var user = await FindUserByEmailOnLoginAsync(loginDTO.Email);
    var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
    if (!result)
        throw new AuthorizationException("Please enter the correct password.", "Login Error");   
 
    var roles = await _userManager.GetRolesAsync(user);
    var singleRole = roles.FirstOrDefault();

    return new GetUserDTO{
        UserName = user.UserName,
        Email = user.Email,
        Role = singleRole
    };
    
}

    //Logout
    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }


}
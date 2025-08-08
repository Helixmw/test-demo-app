using System.Net;
using System.Security.Claims;
using GolfingDataAccessLib.Data;
using GolfingDataAccessLib.Enums;
using GolfingDataAccessLib.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace GolfingDataAccessLib.Services.Auth;
/// <summary>
/// Base class for user Authetication
/// </summary>
public abstract class AuthBaseService{
    protected readonly SignInManager<ApplicationUser> _signInManager;
    protected readonly UserManager<ApplicationUser> _userManager;
    protected readonly RoleManager<IdentityRole> _roleManager;

    public AuthBaseService(SignInManager<ApplicationUser> signInManager,
                            UserManager<ApplicationUser> userManager,
                            RoleManager<IdentityRole> roleManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    //Checks if a email is incorrect on Login
    protected async Task<ApplicationUser> FindUserByEmailOnLoginAsync(string email){
         var user = await _userManager.FindByEmailAsync(email);
          if(user is null) //if email does not exist
                throw new AuthorizationException("Incorrect email address","Incorrect Email");

                return user;
        
    }

    //Checks if a email address already exists on SignUp
    protected async Task FindUserByEmailOnSignUpAsync(string email){
         var user = await _userManager.FindByEmailAsync(email);
          if(user is not null) //if email is already taken
                throw new AuthorizationException("This email address is already registered","Email Already Taken");
        
    }

    //Assigns claims associated with user by email
  protected async Task<ClaimsPrincipal> AssignClaimsAsync(string email, string authenticationType)
{
    var loggedInUser = await _userManager.FindByEmailAsync(email);
    if (loggedInUser is null)
    {
        throw new AuthorizationException("A user with this email was not found", "User Not Found");
    }

    var roles = await _userManager.GetRolesAsync(loggedInUser);
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, loggedInUser.UserName ?? string.Empty),
        new Claim(ClaimTypes.Email, loggedInUser.Email ?? string.Empty),
        new Claim(ClaimTypes.NameIdentifier, loggedInUser.Id)
    };

    // Add roles as claims
    foreach (var role in roles)
    {
        claims.Add(new Claim(ClaimTypes.Role, role));
    }

    // var claimsIdentity = new ClaimsIdentity(claims, authenticationType);
    var claimsIdentity = new ClaimsIdentity(claims);

    return new ClaimsPrincipal(claimsIdentity);
}
}
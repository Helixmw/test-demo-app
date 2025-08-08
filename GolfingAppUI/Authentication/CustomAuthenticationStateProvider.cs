using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GolfingAppUI.Authentication;
using GolfingAppUI.Helpers;
using GolfingAppUI.Models;
using GolfingDataAccessLib.Services.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace GolfingDataAccessLib.Authentication;
/// <summary>
/// Handles user authentication
/// Gets authenticated user from token
/// Deletes cookie and token on logout
/// </summary>
public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
  

    private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    private readonly CustomAuthenticationStateComponent _authComponent;
    public CustomAuthenticationStateProvider(CustomAuthenticationStateComponent authComponent)
    {
    
        _authComponent  = authComponent;
    }

    //Authentication on log in and creates authentication state
    //Assigns token to cookie
    public async Task AuthenticateUser(UserInfo userInfo)
    {
        var token = await _authComponent.Auth(userInfo);
        if(!string.IsNullOrEmpty(token)){
            var readJWT = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var identity = new ClaimsIdentity(readJWT.Claims, "JWT");
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }else{
            //
        }
    }
   

    //Gets authenticated state from then cookie with the token
    //Checks if user claims is not null
    //Deletes token cookie if the claims is null
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {

            try
            {
                var userClaim = await _authComponent.VerifyUser();
                if (userClaim != null)
                {
                    var identity = new ClaimsIdentity(userClaim, "JWT");
                    var user = new ClaimsPrincipal(identity);
                    return await Task.FromResult(new AuthenticationState(user));
                }
                else
                {
                    await _authComponent.DeleteTokenFromCookie();
                    return new AuthenticationState(_anonymous);
                }
            }
            catch (Exception)
            {
                return new AuthenticationState(_anonymous);
            } 
    
        
    }

    //Deletes and removes authentication state on logout
    public async void Logout(){
        ClaimsHelper.Role = string.Empty;
        ClaimsHelper.UserName = string.Empty;
        ClaimsHelper.Email = string.Empty;
        await _authComponent.DeleteTokenFromCookie();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }
    
  

}
using System.Security.Claims;
using System.Text.Encodings.Web;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace GolfingAppUI.Authentication;
/// <summary>
/// Custom Authentication handler that overrides the defaults
/// </summary>
public class CustomAuthenticationHandler : AuthenticationHandler<CustomOptions>
{
    private readonly CustomAuthenticationStateComponent _auth;

    public CustomAuthenticationHandler(IOptionsMonitor<CustomOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        CustomAuthenticationStateComponent auth) : base(options, logger, encoder)
    {
        _auth = auth;
    }

    //Gets token and handles authentication by role
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string token = Request.Cookies["auth_token"];
        if (string.IsNullOrEmpty(token))
            return AuthenticateResult.Fail("Authentication Failed");

        var userClaims = _auth.VerifyUser(token);
        if(userClaims == null)
            return AuthenticateResult.Fail("Authentication Failed");

        var principal = new ClaimsPrincipal(new ClaimsIdentity(userClaims, "JWT"));
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }

    //When user is not authenticated under any user profile, redirects them to the log in page
    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Context.Response.Redirect("/Login");
        return Task.CompletedTask;
    }

    //When a user is authenticated under a different profile and is forbidden to access
    //certain pages
    protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        Context.Response.Redirect("/Home/Accessdenied"); //Or access denied page
        return Task.CompletedTask;
    }
}

public class CustomOptions : AuthenticationSchemeOptions
{
    
}
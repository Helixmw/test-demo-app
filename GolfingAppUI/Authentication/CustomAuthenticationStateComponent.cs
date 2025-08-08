using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GolfingAppUI.Models;
using Microsoft.IdentityModel.Tokens;

namespace GolfingAppUI.Authentication;
/// <summary>
/// Generates cookies and tokens
/// </summary>
public class CustomAuthenticationStateComponent
{

    private readonly IConfiguration configuration;
    private readonly CookieService _cookieService;

    private readonly string cookieKey = "auth_token";
    public CustomAuthenticationStateComponent(IConfiguration configuration, CookieService cookieService)
    {
        this.configuration = configuration;
        _cookieService = cookieService;
    }
   

    //Authenticates user by generating tokens and send a token cookie
    public async Task<string> Auth(UserInfo user){
        //Generate Token
        var token = GenerateJWT(user);

        //Set cookies on client
        await _cookieService.SetCookieAsync(cookieKey, token, 30);
        return token;
    }

  
    //Generates Token to be send as a cookie
    private string GenerateJWT(UserInfo user){
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim("UserName", user.UserName),
            new Claim("Email", user.Email),
            new Claim("Role", user.Role)
        };
        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    //Checks if user has token on the client side
    public async Task<IEnumerable<Claim>?> VerifyUser()
    {
        var token = await _cookieService.GetCookieAsync(cookieKey);
        if(token == null) 
        return null;
       

        return VerifyUser(token);
    }

    //Validates the token when taken from user
    public IEnumerable<Claim>? VerifyUser(string token)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
    var tokenHandler = new JwtSecurityTokenHandler();

    var validationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = securityKey
    };

    try
    {
        tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
        var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
        if (jsonToken != null)
        {
            return jsonToken.Claims.ToList();
        }
    }
    catch (Exception)
    {
    }

    return null;
    }

    public async Task DeleteTokenFromCookie()
    {
        await _cookieService.DeleteCookieAsync(cookieKey);
    }

  


}
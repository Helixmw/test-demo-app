using System.ComponentModel.DataAnnotations;

namespace GolfingAppUI.Models;

public class RegisterUser{

    [Required(ErrorMessage = "Please provide a username")]
    public string? Username { get; set;}

    [Required(ErrorMessage = "Please provide your email address")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address")]
    public string? Email { get; set;}

    [PasswordRequirement]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Please confirm your password")]
    [Compare("Password", ErrorMessage = "Your password does not match")]
    public string? ConfirmPassword { get; set; }
}
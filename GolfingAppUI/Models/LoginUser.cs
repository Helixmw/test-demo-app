using System.ComponentModel.DataAnnotations;

namespace GolfingAppUI.Models;

public class LoginUser{

    [Required(ErrorMessage = "Please provide your email address")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address")]
    public string? Email { get; set;}

    [Required(ErrorMessage = "Please enter your password")]
    public string? Password { get; set; }

}
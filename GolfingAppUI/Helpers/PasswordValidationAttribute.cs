using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class PasswordRequirementAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var password = value as string;
        if (string.IsNullOrEmpty(password))
        {
            return new ValidationResult("Please provide a password");
        }

        // Regular expression for the password requirements
        var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z]).{6,}$");
        if (!regex.IsMatch(password))
        {
            return new ValidationResult("Password must be at least 6 characters long, with at least one lowercase and one uppercase letter.");
        }

        return ValidationResult.Success;
    }
}
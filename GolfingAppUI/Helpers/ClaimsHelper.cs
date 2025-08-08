namespace GolfingAppUI.Helpers;

public static class ClaimsHelper
{
   public static string UserName {get; set;}
   public static string Email {get; set;}
   public static string Role {get; set;}

   public static void SetClaims(string userName, string email, string role)
   {
       UserName = userName;
       Email = email;
       Role = role;
   }
}
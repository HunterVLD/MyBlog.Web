using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models.ViewModels;

public class LoginRequest
{
    [Microsoft.Build.Framework.Required]
    public string Username { get; set; }
    [Microsoft.Build.Framework.Required]
    [MinLength(6, ErrorMessage = "The password must be with at least 6 chars!")]
    public string Password { get; set; }

    public string? returnUrl { get; set; }
}
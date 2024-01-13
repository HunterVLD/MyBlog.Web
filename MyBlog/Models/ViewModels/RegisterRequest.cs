using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models.ViewModels;

public class RegisterRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(6, ErrorMessage = "Password has to be at least 6 chars!")]
    public string Password { get; set; }
}
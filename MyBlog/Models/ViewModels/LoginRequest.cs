using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models.ViewModels;

public class LoginRequest
{
    [Required]
    [MinLength(3, ErrorMessage = "At least 3 chars!")]
    [MaxLength(15, ErrorMessage = "The Username cant have more than 15 chars!")]
    public string Username { get; set; }
    [Required]
    [MinLength(6, ErrorMessage = "The password must be with at least 6 chars!")]
    [MaxLength(15, ErrorMessage = "The password cant have more than 15 chars!")]
    public string Password { get; set; }

    public string? returnUrl { get; set; }
}
using System.ComponentModel.DataAnnotations;
using MyBlog.CustomValidators;

namespace MyBlog.Models.ViewModels;

public class LoginRequest
{
    [Required]
    [MinLength(3, ErrorMessage = "At least 3 chars!")]
    [MaxLength(15, ErrorMessage = "The Username cant have more than 15 chars!")]
    [UsernameByRegex(ErrorMessage = "Invalid characters are used")]
    public string Username { get; set; }
    [Required]
    [MinLength(8, ErrorMessage = "The password must be with at least 8 chars!")]
    [MaxLength(60, ErrorMessage = "The password cant have more than 60 chars!")]
    [EmailByRegex(ErrorMessage = "Invalid characters are used")]
    public string Password { get; set; }

    public string? returnUrl { get; set; }
}
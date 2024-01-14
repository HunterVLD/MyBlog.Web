using System.ComponentModel.DataAnnotations;
using MyBlog.CustomValidators;

namespace MyBlog.Models.ViewModels;

public class RegisterRequest
{
    [Required]
    [MinLength(5, ErrorMessage = "At least 3 chars!")]
    [MaxLength(20, ErrorMessage = "The Username cant have more than 15 chars!")]
    [UsernameByRegex(ErrorMessage = "Invalid characters are used!")]
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    [MinLength(12, ErrorMessage = "Min lenght 12 chars!")]
    [MaxLength(252, ErrorMessage = "The Username cant have more than 15 chars!")]
    [EmailByRegex(ErrorMessage = "Invalid characters are used")]
    public string Email { get; set; }
    [Required]
    [MinLength(8, ErrorMessage = "Password has to be at least 8 chars!")]
    [MaxLength(60, ErrorMessage = "The password cant have more than 60 chars!")]
    [PasswordByRegex(ErrorMessage = "Invalid characters are used")]
    public string Password { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models.ViewModels;

public class RegisterRequest
{
    [Required]
    [MinLength(3, ErrorMessage = "At least 3 chars!")]
    [MaxLength(15, ErrorMessage = "The Username cant have more than 15 chars!")]
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    [MinLength(12, ErrorMessage = "Min lenght 12 chars!")]
    [MaxLength(252, ErrorMessage = "The Username cant have more than 15 chars!")]
    public string Email { get; set; }
    [Required]
    [MinLength(6, ErrorMessage = "Password has to be at least 6 chars!")]
    [MaxLength(15, ErrorMessage = "The password cant have more than 15 chars!")]
    public string Password { get; set; }
}
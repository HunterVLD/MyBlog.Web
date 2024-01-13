using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models.ViewModels.Users;

public class UserViewModel
{
    //for display users
    public List<User> Users { get; set; }
    
    //for add new users
    [Required]
    public string UserName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 chars!")]
    public string Password { get; set; }
    public bool AdminRoleCheckbox { get; set; }
}
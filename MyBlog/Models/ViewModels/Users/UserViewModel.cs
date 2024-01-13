namespace MyBlog.Models.ViewModels.Users;

public class UserViewModel
{
    //for display users
    public List<User> Users { get; set; }
    
    //for add new users
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool AdminRoleCheckbox { get; set; }
}
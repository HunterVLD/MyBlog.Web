namespace MyBlog.Models.ViewModels.Users;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string EmailAddress { get; set; }
    public string[] RolesNames { get; set; }
}
namespace MyBlog.Models.Domain;

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }

    //many to many relationShip\
    public ICollection<BlogPost> BlogPosts { get; set; }
}
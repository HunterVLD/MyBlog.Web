using MyBlog.Models.Domain;

namespace MyBlog.Models.ViewModels;

public class HomeMergeRequest
{
    public IEnumerable<Tag> tags { get; set; }
    public IEnumerable<BlogPost> blogPosts { get; set; }
}
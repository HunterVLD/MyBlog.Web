namespace MyBlog.Models.ViewModels.ForBlogPosts;

public class AddLikeRequest
{
    public Guid BlogPostId { get; set; }
    public Guid UserId { get; set; }
}
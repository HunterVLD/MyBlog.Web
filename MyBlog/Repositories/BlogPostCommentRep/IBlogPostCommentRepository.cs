using MyBlog.Models.Domain;
using MyBlog.Models.ViewModels.ForBlogPosts;

namespace MyBlog.Repositories.BlogPostCommentRep;

public interface IBlogPostCommentRepository
{
    Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);
    Task<IEnumerable<BlogPostComment>> GetAllByIdAsync(Guid id);
    Task<bool> DeleteAllUserCommentsByIdAsync(Guid id);
    
    //misc
    BlogDetailsRequest? MappingToViewModel(BlogPost blogPost, int totalLikesAmount, bool userLikedPost,
        List<BlogComment> commentsForView);
}
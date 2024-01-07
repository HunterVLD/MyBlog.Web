using MyBlog.Models.Domain;

namespace MyBlog.Repositories.BlogPostCommentRep;

public interface IBlogPostCommentRepository
{
    Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);
    Task<IEnumerable<BlogPostComment>> GetAllByIdAsync(Guid id);
}
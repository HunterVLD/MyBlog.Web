using Microsoft.AspNetCore.Mvc;
using MyBlog.Models.Domain;

namespace MyBlog.Repositories.BlogPostLikes;

public interface IBlogPostLikeRepository
{
    Task<int> GetTotalLikesAsync(Guid blogPostId);
    Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);
    Task<BlogPostLike> AddLikeForBlogAsync(BlogPostLike blogPostLike);
    Task<bool> IsUserAlreadyLikedThisPost(IEnumerable<BlogPostLike> likes, string? id);
}
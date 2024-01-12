using Microsoft.AspNetCore.Mvc;
using MyBlog.Models.Domain;

namespace MyBlog.Repositories.BlogPostLikes;

public interface IBlogPostLikeRepository
{
    Task<int> GetTotalLikesAsync(Guid blogPostId);
    Task<BlogPostLike?> GetUserLikeIdForCurrentPost(Guid postId, Guid userId);
    Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);
    Task<BlogPostLike?> AddLikeForBlogAsync(BlogPostLike blogPostLike);
    Task<bool> DeleteLikeForBlogAsync(Guid id);
    Task<bool> IsUserAlreadyLikedThisPost(IEnumerable<BlogPostLike> likes, string? id);
}
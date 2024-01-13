using Microsoft.AspNetCore.Mvc;
using MyBlog.Models.Domain;

namespace MyBlog.Repositories.BlogPostLikes;

public interface IBlogPostLikeRepository
{
    Task<int> GetTotalLikesAsync(Guid blogPostId);
    Task<BlogPostLike?> GetUserLikeIdForCurrentPostAsync(Guid postId, Guid userId);
    Task<IEnumerable<BlogPostLike>> GetLikesForBlogAsync(Guid blogPostId);
    Task<BlogPostLike?> AddLikeForBlogAsync(BlogPostLike blogPostLike);
    Task<bool> DeleteLikeForBlogAsync(Guid id);
    Task<bool> DeleteAllLikesFromUserIdAsync(Guid id);
    Task<bool> IsUserAlreadyLikedThisPostAsync(IEnumerable<BlogPostLike> likes, string? id);
}
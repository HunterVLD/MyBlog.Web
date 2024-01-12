using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Models.Domain;

namespace MyBlog.Repositories.BlogPostLikes;

public class BlogPostLikeRepository : IBlogPostLikeRepository
{
    private readonly MyBlogDbContext _dbContext;

    public BlogPostLikeRepository(MyBlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<int> GetTotalLikesAsync(Guid blogPostId)
    {
        return await _dbContext.BlogPostLike.CountAsync(x => x.BlogPostId == blogPostId);
    }

    public async Task<BlogPostLike?> GetUserLikeIdForCurrentPost(Guid postId, Guid userId)
    {
        return await _dbContext.BlogPostLike.FirstOrDefaultAsync(x => x.BlogPostId == postId && x.UserId == userId);
    }

    public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
    {
        return await _dbContext.BlogPostLike.Where(x => x.BlogPostId == blogPostId).ToListAsync();
    }

    public async Task<BlogPostLike?> AddLikeForBlogAsync(BlogPostLike blogPostLike)
    {
        var existingLike = await _dbContext.BlogPostLike.FirstOrDefaultAsync(
            x => x.BlogPostId == blogPostLike.BlogPostId && x.UserId == blogPostLike.UserId);

        if (existingLike != null) {
            return null;
        }
        
        await _dbContext.BlogPostLike.AddAsync(blogPostLike);
        await _dbContext.SaveChangesAsync();

        return blogPostLike;
    }

    public async Task<bool> DeleteLikeForBlogAsync(Guid id)
    {
        var like = await _dbContext.BlogPostLike.FirstOrDefaultAsync(x => x.Id == id);

        if (like != null) {
            _dbContext.BlogPostLike.Remove(like);
            await _dbContext.SaveChangesAsync();
            
            return true;
        }

        return false;
    }

    public Task<bool> IsUserAlreadyLikedThisPost(IEnumerable<BlogPostLike> likes, string? userId)
    {
        if (string.IsNullOrEmpty(userId)) {
            throw new NullReferenceException();
        }

        var likeFromUser = likes.FirstOrDefault(x => x.UserId == Guid.Parse(userId));

        return Task.FromResult(likeFromUser != null);
    }
}
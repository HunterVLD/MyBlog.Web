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

    public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
    {
        return await _dbContext.BlogPostLike.Where(x => x.BlogPostId == blogPostId).ToListAsync();
    }

    //todo add delete ike functionality
    public async Task<BlogPostLike> AddLikeForBlogAsync(BlogPostLike blogPostLike)
    {
        await _dbContext.BlogPostLike.AddAsync(blogPostLike);
        await _dbContext.SaveChangesAsync();

        return blogPostLike;
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
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Models.Domain;

namespace MyBlog.Repositories.BlogPostCommentRep;

public class BlogPostCommentRepository : IBlogPostCommentRepository
{
    private readonly MyBlogDbContext _dbContext;

    public BlogPostCommentRepository(MyBlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
    {
        await _dbContext.BlogPostComment.AddAsync(blogPostComment);
        await _dbContext.SaveChangesAsync();

        return blogPostComment;
    }

    public async Task<IEnumerable<BlogPostComment>> GetAllByIdAsync(Guid id)
    {
        return await _dbContext.BlogPostComment
            .Where(x => x.BlogPostId == id)
            .ToListAsync();
    }

    public async Task<bool> DeleteAllUserCommentsByIdAsync(Guid id)
    {
        var allComments = await _dbContext.BlogPostComment
            .Where(x => x.UserId == id)
            .ToListAsync();

        if (allComments != null && allComments.Any()) {
            foreach (var comment in allComments) {
                _dbContext.BlogPostComment.Remove(comment);
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }

        return false;
    }
}
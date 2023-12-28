using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Models.Domain;

namespace MyBlog.Repositories;

public class TagRepository : ITagRepository
{
    private readonly MyBlogDbContext _dbContext;
    
    public TagRepository(MyBlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Getters
    public async Task<IEnumerable<Tag?>?> GetAllAsync()
    {
        return await _dbContext.Tags.ToListAsync();
    }

    public async Task<Tag?> GetByIdAsync(Guid id)
    {
        var tag = await _dbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

        return tag;
    }
    #endregion

    #region CRUD
    public async Task<Tag> AddAsync(Tag tag)
    {
        await _dbContext.Tags.AddAsync(tag);
        await _dbContext.SaveChangesAsync();

        return tag;
    }

    public async Task<Tag?> UpdateAsync(Tag tag)
    {
        var existingTag = await _dbContext.Tags.FindAsync(tag.Id);

        if (existingTag != null) {
            existingTag.Name = tag.Name;
            existingTag.DisplayName = tag.DisplayName;

            await _dbContext.SaveChangesAsync();

            return existingTag;
        }

        return null;
    }

    public async Task<Tag?> DeleteAsync(Guid id)
    {
        var tag = await _dbContext.Tags.FindAsync(id);

        if (tag != null) {
            _dbContext.Tags.Remove(tag);
            await _dbContext.SaveChangesAsync();
        }

        return tag;
    }
    #endregion

    
}
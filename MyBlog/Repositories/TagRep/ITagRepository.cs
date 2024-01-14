using MyBlog.Models.Domain;

namespace MyBlog.Repositories;

//For CRUD Operations
public interface ITagRepository
{
    Task<IEnumerable<Tag?>?> GetAllAsync();
    Task<Tag?> GetByIdAsync(Guid id);
    Task<Tag> AddAsync(Tag tag);
    Task<Tag?> UpdateAsync(Tag tag);
    Task<Tag?> DeleteAsync(Guid id);
}
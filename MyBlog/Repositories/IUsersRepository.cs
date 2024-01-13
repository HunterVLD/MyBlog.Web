using Microsoft.AspNetCore.Identity;

namespace MyBlog.Repositories;

public interface IUsersRepository
{
    Task<IEnumerable<IdentityUser>> GetAllAsync();
}
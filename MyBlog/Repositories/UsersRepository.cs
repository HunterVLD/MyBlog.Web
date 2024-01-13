using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;

namespace MyBlog.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly AuthDbContext _authDbContext;

    public UsersRepository(AuthDbContext authDbContext)
    {
        _authDbContext = authDbContext;
    }
    
    public async Task<IEnumerable<IdentityUser>> GetAllAsync()
    {
        //without superUserIn @Where@
        return await _authDbContext.Users
            .Where(x => x.Email != "my_odyssey_blog@odmyblog.com")
            .ToListAsync();
    }
}
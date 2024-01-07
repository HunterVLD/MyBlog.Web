using Microsoft.EntityFrameworkCore;
using MyBlog.Models.Domain;

namespace MyBlog.Data;

public class MyBlogDbContext : DbContext
{
    //DB tables
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    
    public DbSet<BlogPostLike> BlogPostLike { get; set; }
    public DbSet<BlogPostComment> BlogPostComment { get; set; }

    public MyBlogDbContext(DbContextOptions<MyBlogDbContext> options) : base(options)
    {
        
    }
}
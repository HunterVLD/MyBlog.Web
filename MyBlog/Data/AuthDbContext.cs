using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Data;

public class AuthDbContext : IdentityDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        //seed Roles(B_User, A_User, S_User)
        var basicUserRoleId = "55a50f32-1648-4801-aca4-865dff7ccccd"; //Guid.NewGuid.ToString
        var adminUserRoleId = "996de80b-6837-4b25-a5ac-2d984c16bffc";
        var superAdminUserRoleId = "bc541009-289f-4be7-87ac-c5e4e181c6f2";

        var roles = new List<IdentityRole> {
            new() {
                Name = "User",
                NormalizedName = "User",
                Id = basicUserRoleId,
                ConcurrencyStamp = basicUserRoleId
            },
            new() {
                Name = "Admin",
                NormalizedName = "Admin",
                Id = adminUserRoleId,
                ConcurrencyStamp = adminUserRoleId
            },
            new() {
                Name = "SuperAdmin",
                NormalizedName = "SuperAdmin",
                Id = superAdminUserRoleId,
                ConcurrencyStamp = superAdminUserRoleId
            }
        };

        builder.Entity<IdentityRole>().HasData(roles);

        //Create SuperAdminUser
        var superAdminId = "12e864c1-f418-4539-b032-c3cc1b89bc57";
        var superAdminUser = new IdentityUser {
            UserName = "Odyssey_myBlog",
            Email = "my_odyssey_blog@odmyblog.com",
            NormalizedEmail = "my_odyssey_blog@odmyblog.com".ToUpper(),
            NormalizedUserName = "Odyssey_myBlog".ToUpper(),
            Id = superAdminId
        };

        superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
            .HashPassword(superAdminUser, "SuperAdmin!V22!");

        builder.Entity<IdentityUser>().HasData(superAdminUser);
        
        //Add all Roles To SuperAdminUser
        var superAdminRoles = new List<IdentityUserRole<string>>() {
            //pare by id-s
            new() {
                RoleId = superAdminUserRoleId,
                UserId = superAdminId
            },
            new() {
                RoleId = adminUserRoleId,
                UserId = superAdminId
            },
            new() {
                RoleId = basicUserRoleId,
                UserId = superAdminId
            },
        };

        builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
    }
}
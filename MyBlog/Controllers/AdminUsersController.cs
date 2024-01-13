using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data;
using MyBlog.Models.ViewModels.Users;
using MyBlog.Repositories;
using MyBlog.Repositories.BlogPostCommentRep;
using MyBlog.Repositories.BlogPostLikes;

namespace MyBlog.Controllers;

[Authorize(Roles = "Admin")]
public class AdminUsersController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IBlogPostLikeRepository _blogPostLikeRepository;
    private readonly IBlogPostCommentRepository _blogPostCommentRepository;
    private readonly IUsersRepository _usersRepository;

    public AdminUsersController(IUsersRepository usersRepository, UserManager<IdentityUser> userManager,
        IBlogPostLikeRepository blogPostLikeRepository, IBlogPostCommentRepository blogPostCommentRepository)
    {
        _usersRepository = usersRepository;
        _userManager = userManager;
        _blogPostLikeRepository = blogPostLikeRepository;
        _blogPostCommentRepository = blogPostCommentRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var allUsers = await _usersRepository.GetAllAsync();

        var userViewModel = new UserViewModel {
            Users = new List<User>()
        };
        
        foreach (var user in allUsers) {
            userViewModel.Users.Add(new User {
                Id = Guid.Parse(user.Id),
                Username = user.UserName,
                EmailAddress = user.Email,
                RolesNames = (await _userManager.GetRolesAsync(user)).ToArray()
            });
        }
        
        return View(userViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> List(UserViewModel request)
    {
        var identityUser = new IdentityUser {
            UserName = request.UserName,
            Email = request.Email,
        };

        var identityResult =
            await _userManager.CreateAsync(identityUser, request.Password);

        if (identityResult.Succeeded) {
            var roles = new List<string> { "User" };

            if (request.AdminRoleCheckbox && User.IsInRole("SuperAdmin"))
                roles.Add("Admin");

            var result = await _userManager.AddToRolesAsync(identityUser, roles);

            if (result.Succeeded)
                return RedirectToAction("List");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid userToDeleteId)
    {
        var userToDelete = await _userManager.FindByIdAsync(userToDeleteId.ToString());

        if (userToDelete != null) {
            string[] rolesUserToDelete = (await _userManager.GetRolesAsync(userToDelete)).ToArray();

            foreach (var role in rolesUserToDelete) {
                if(role == "SuperAdmin" || (role == "Admin" && !User.IsInRole("SuperAdmin")))
                    return RedirectToAction("Index", "Home");
            }
            
            await _blogPostLikeRepository.DeleteAllLikesFromUserIdAsync(Guid.Parse(userToDelete.Id));
            await _blogPostCommentRepository.DeleteAllUserCommentsByIdAsync(Guid.Parse(userToDelete.Id));
            
            var result = await _userManager.DeleteAsync(userToDelete);

            if (result.Succeeded)
                return RedirectToAction("List");
        }

        return RedirectToAction("Index", "Home");
    }
}
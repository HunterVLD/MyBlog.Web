using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Models.ViewModels;

namespace MyBlog.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly AuthDbContext _authDbContext;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        AuthDbContext authDbContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authDbContext = authDbContext;
    }

    #region Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (ModelState.IsValid) {
            if (await _authDbContext.Users.AnyAsync(u => u.UserName == request.Username)) {
                ModelState.AddModelError("Username", "This Name is Already Exists!");
                
                return View();
            }

            if (await _authDbContext.Users.AnyAsync(u => u.Email == request.Email)) {
                ModelState.AddModelError("Email", "This Email is already have registered account");

                return View();
            }
            
            var user = new IdentityUser {
                UserName = request.Username,
                Email = request.Email,
            };
        
            var registerResult = await _userManager.CreateAsync(user, request.Password);
        
            if (registerResult.Succeeded) {
                var pasteRoleResult = await _userManager.AddToRoleAsync(user, "User");

                if (pasteRoleResult.Succeeded) {
                    await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);
                
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        
        return View();
    }

    #endregion

    #region Login
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        //ASP auto return the URL, witch we wanna open before login page to our func(string returnUrl)
        var model = new LoginRequest {
            returnUrl = returnUrl
        };
        
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (ModelState.IsValid) {
            var signInResult = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

            if (signInResult.Succeeded) {

                if (!string.IsNullOrEmpty(request.returnUrl)) {
                    return Redirect(request.returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
        }

        return View();
    }
    

    #endregion

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }

    //magic shit
    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
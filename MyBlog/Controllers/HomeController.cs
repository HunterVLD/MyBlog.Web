using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using MyBlog.Models.ViewModels;
using MyBlog.Repositories;
using MyBlog.Repositories.BlogPostRep;

namespace MyBlog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITagRepository _tagRepository;
    private readonly IBlogPostRepository _blogPostRepository;
    public HomeController(ILogger<HomeController> logger, ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
    {
        _logger = logger;
        _tagRepository = tagRepository;
        _blogPostRepository = blogPostRepository;
    }

    public async Task<IActionResult> Index()
    {
        var allPosts = (await _blogPostRepository.GetAllAsync())
            .Where(x => x.IsVisible)
            .OrderByDescending(x => x.PublishedDate);

        var allTags = await _tagRepository.GetAllAsync();

        var mergeModels = new HomeMergeRequest {
            tags = allTags,
            blogPosts = allPosts
        };
        
        return View(mergeModels);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
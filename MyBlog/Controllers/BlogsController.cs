using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models.ViewModels.ForBlogPosts;
using MyBlog.Repositories.BlogPostLikes;
using MyBlog.Repositories.BlogPostRep;

namespace MyBlog.Controllers;

public class BlogsController : Controller
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IBlogPostLikeRepository _blogPostLikeRepository;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLikeRepository,
        SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _blogPostRepository = blogPostRepository;
        _blogPostLikeRepository = blogPostLikeRepository;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {
        var blogPost = await _blogPostRepository.GetByUrlAsync(urlHandle);
        var viewPost = new BlogDetailsRequest();
        bool userLikedPost = false;
        
        if (blogPost != null) {
            var totalLikesAmount = await _blogPostLikeRepository.GetTotalLikesAsync(blogPost.Id);

            //check for user is signed in for checking is user liked blog post
            if (_signInManager.IsSignedIn(User)) {
                var allLikesFromThisBlog =
                    await _blogPostLikeRepository.GetLikesForBlog(blogPost.Id);

                userLikedPost = await _blogPostLikeRepository.IsUserAlreadyLikedThisPost(allLikesFromThisBlog,
                    _userManager.GetUserId(User));
            }

            //todo TO MAP FUNC
            viewPost = new BlogDetailsRequest
            {
                Id = blogPost.Id,
                Content = blogPost.Content,
                PageTitle = blogPost.PageTitle,
                Author = blogPost.Author,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                Heading = blogPost.Heading,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                UrlHandle = blogPost.UrlHandle,
                IsVisible = blogPost.IsVisible,
                Tags = blogPost.Tags,
                TotalLikes = totalLikesAmount,
                IsLikedByCurrentUser = userLikedPost
            };
        }
        
        
        
        return View(viewPost);
    }
}
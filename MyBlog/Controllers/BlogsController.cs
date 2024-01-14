using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models.Domain;
using MyBlog.Models.ViewModels.ForBlogPosts;
using MyBlog.Repositories.BlogPostCommentRep;
using MyBlog.Repositories.BlogPostLikes;
using MyBlog.Repositories.BlogPostRep;

namespace MyBlog.Controllers;

public class BlogsController : Controller
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IBlogPostLikeRepository _blogPostLikeRepository;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IBlogPostCommentRepository _blogPostCommentRepository;

    public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLikeRepository,
        SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
        IBlogPostCommentRepository blogPostCommentRepository)
    {
        _blogPostRepository = blogPostRepository;
        _blogPostLikeRepository = blogPostLikeRepository;
        _signInManager = signInManager;
        _userManager = userManager;
        _blogPostCommentRepository = blogPostCommentRepository;
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
                    await _blogPostLikeRepository.GetLikesForBlogAsync(blogPost.Id);

                userLikedPost = await _blogPostLikeRepository.IsUserAlreadyLikedThisPostAsync(allLikesFromThisBlog,
                    _userManager.GetUserId(User));
            }

            //GET COMMENTS FOR BLOG
            var blogCommentsDomain = await _blogPostCommentRepository.GetAllByIdAsync(blogPost.Id);

            var commentsForView = new List<BlogComment>();
            foreach (var blogComment in blogCommentsDomain) {
                commentsForView.Add(new BlogComment {
                    Description = blogComment.Description,
                    DateAdded = blogComment.DateAdded,
                    UserName = (await _userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName
                });
            }

            viewPost = _blogPostCommentRepository.MappingToViewModel(blogPost, totalLikesAmount, userLikedPost,
                commentsForView);
        }
        
        return View(viewPost);
    }

    [HttpPost]
    public async Task<IActionResult> Index(BlogDetailsRequest blogDetailsRequest)
    {
        if (ModelState.IsValid) {
            if (_signInManager.IsSignedIn(User)) {
                var modelForSave = new BlogPostComment {
                    BlogPostId = blogDetailsRequest.Id,
                    Description = blogDetailsRequest.CommentsDescription,
                    UserId = Guid.Parse(_userManager.GetUserId(User)),
                    DateAdded = DateTime.Now
                };
            
                await _blogPostCommentRepository.AddAsync(modelForSave);

                return RedirectToAction("Index", 
                    new { urlHandle = blogDetailsRequest.UrlHandle });
            }
        }
        else {
            foreach (var key in ModelState.Keys)
            {
                var entry = ModelState[key];
                if (entry.Errors.Any())
                {
                    var errorMessage = entry.Errors.First().ErrorMessage;
                    Console.WriteLine("\n\n\n " +errorMessage);
                }
            }
        }

        return View();
    }
}
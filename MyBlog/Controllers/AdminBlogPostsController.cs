using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBlog.Models.ViewModels.ForBlogPosts;
using MyBlog.Repositories;
using MyBlog.Repositories.BlogPostRep;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace MyBlog.Controllers;

[Authorize(Roles = "Admin")]
public class AdminBlogPostsController : Controller
{
    private readonly ITagRepository _tagRepository;
    private readonly IBlogPostRepository _blogPostRepository;

    public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
    {
        _tagRepository = tagRepository;
        _blogPostRepository = blogPostRepository;
    }

    #region AddMethods
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        //get tags from repos
        var tags = await _tagRepository.GetAllAsync();

        if (tags != null) {
            var model = new AddBlogPostRequest {
                Tags = tags.Select(x => new SelectListItem {
                    Text = x.DisplayName,
                    Value = x.Id.ToString()
                })
            };
        
            //DISPLAYING ALL CREATED TAGS IN MODEL
            return View(model);
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
    {
        //CHECKING FOR NULL
        var resultOfValidation = new List<ValidationResult>();
        var contextValidation = new ValidationContext(addBlogPostRequest, null, null);
        
        //todo ADD TEXT ON VIEW
        if (!Validator.TryValidateObject(addBlogPostRequest, contextValidation, resultOfValidation, true))
            return RedirectToAction("Add");
        
        //GOING TO BASIC FUNCTIONALITY
        var newBlogPost = await _blogPostRepository.MappingToBlogPost(addBlogPostRequest, _tagRepository);
        await _blogPostRepository.AddAsync(newBlogPost);

        return RedirectToAction("Add");
    }
    

    #endregion
    
    #region MiscMethods
    [HttpGet]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        await _blogPostRepository.DeleteAsync(id);

        return RedirectToAction("ListOfBlogs");
    }
    
    [HttpGet]
    public async Task<IActionResult> ListOfBlogs()
    {
        //call RepositoryForGet all data
        var allTags = await _blogPostRepository.GetAllAsync();
        
        return View(allTags);
    }

    #endregion
    
    #region EditMethods
    [HttpGet]
    public async Task<IActionResult> EditPost(Guid id)
    {
        var post = await _blogPostRepository.GetByIdAsync(id);
        
        if (post != null) {
            var postToView = await _blogPostRepository.MappingToViewModel(post, _tagRepository);

            if (postToView != null)
                return View(postToView);
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EditPost(EditBlogPostRequest editBlogPostRequest)
    {
        //CHECKING FOR NULL
        var resultOfValidation = new List<ValidationResult>();
        var contextValidation = new ValidationContext(editBlogPostRequest, null, null);
        
        if (!Validator.TryValidateObject(editBlogPostRequest, contextValidation, resultOfValidation, true))
            return RedirectToAction("EditPost");
        
        //CONTINUE BASIC funcs
        var editedBlogPost = await _blogPostRepository.MappingToBlogPost(editBlogPostRequest, _tagRepository);
        
        Console.WriteLine((editedBlogPost == null) + "======================================= after mapping");
        Console.WriteLine((await _blogPostRepository.UpdateAsync(editedBlogPost) == null) + "======================================= after updating");

        return RedirectToAction("ListOfBlogs");
    }

    #endregion
}
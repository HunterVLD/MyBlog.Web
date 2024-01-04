using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models.Domain;
using MyBlog.Models.ViewModels.ForTags;
using MyBlog.Repositories;

namespace MyBlog.Controllers;

[Authorize(Roles = "Admin")]
public class AdminTagsController : Controller
{
    private readonly ITagRepository _tagRepository;
    
    public AdminTagsController(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    #region AddMethods
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(AddTagRequest addTagRequest)
    {
        //Add new Tag In DB using Repository Pattern
        await _tagRepository.AddAsync(new Tag {
            Name = addTagRequest.NameOfTag,
            DisplayName = addTagRequest.DisplayName
        });

        return RedirectToAction("Add");
    }

    #endregion

    #region MiscMethods
    [HttpGet]
    public async Task<IActionResult> DeleteTag(Guid id)
    {
        await _tagRepository.DeleteAsync(id);

        return RedirectToAction("ListOfTags");
    }
    
    [HttpGet]
    public async Task<IActionResult> ListOfTags()
    {
        var allTags = await _tagRepository.GetAllAsync();
        
        return View(allTags);
    }
    

    #endregion
    
    #region EditMethods
    [HttpGet]
    public async Task<IActionResult> EditTag(Guid id)
    {
        var tag = await _tagRepository.GetByIdAsync(id);

        if (tag != null) {
            var editTagRequest = new EditTagRequest {
                Id = tag.Id,
                NameOfTag = tag.Name,
                DisplayName = tag.DisplayName
                
            };

            return View(editTagRequest);
        }
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EditTag(EditTagRequest editTagRequest)
    {
        await _tagRepository.UpdateAsync(new Tag {
            Id = editTagRequest.Id,
            Name = editTagRequest.NameOfTag,
            DisplayName = editTagRequest.DisplayName
        });

        return RedirectToAction("ListOfTags");
    }

    #endregion
}
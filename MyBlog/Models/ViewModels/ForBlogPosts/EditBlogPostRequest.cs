using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyBlog.Models.ViewModels.ForBlogPosts;

public class EditBlogPostRequest
{
    [Required]
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    public Guid Id { get; set; }
    [Required] 
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    public string Heading { get; set; } 
    [Required]
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    public string PageTitle { get; set; } 
    [Required] 
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    public string Content { get; set; }
    [Required] 
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    public string ShortDescription { get; set; }
    [Required] 
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    public string FeaturedImageUrl { get; set; }
    [Required] 
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    public string UrlHandle { get; set; }
    [Required] 
    public DateTime PublishedDate { get; set; } 
    [Required] 
    public string Author { get; set; } 
    public bool IsVisible { get; set; }
    
    // Display All created Tags
    public IEnumerable<SelectListItem> Tags { get; set; }
    
    //Collected Tags
    public string[] SelectedTags { get; set; } = Array.Empty<string>();
}
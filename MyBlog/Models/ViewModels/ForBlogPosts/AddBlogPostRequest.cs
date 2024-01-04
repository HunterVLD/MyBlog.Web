using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyBlog.Models.ViewModels.ForBlogPosts;

public class AddBlogPostRequest
{
    [Required] public string Heading { get; set; } 
    [Required] public string PageTitle { get; set; } 
    [Required] public string Content { get; set; }
    [Required] public string ShortDescription { get; set; }
    [Required] public string FeaturedImageUrl { get; set; }
    [Required] public string UrlHandle { get; set; }
    [Required] public DateTime PublishedDate { get; set; } 
    [Required] public string Author { get; set; }
    public bool IsVisible { get; set; }
    
    // Display All created Tags
    public IEnumerable<SelectListItem> Tags { get; set; }
    
    
    //Collected Tags
    [Required] public string[] SelectedTags { get; set; } = Array.Empty<string>();
    
}
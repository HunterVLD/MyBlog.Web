using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyBlog.Models.ViewModels.ForBlogPosts;

public class AddBlogPostRequest
{
    [Required] 
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    [MaxLength(25, ErrorMessage = "Max number of lenght is 25")]
    public string Heading { get; set; } 
    [Required] 
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    [MaxLength(25, ErrorMessage = "Max number of lenght is 25")]
    public string PageTitle { get; set; } 
    [Required] 
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    [MaxLength(2000, ErrorMessage = "Max number of lenght is 2000")]
    public string Content { get; set; }
    [Required]
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    [MaxLength(25, ErrorMessage = "Max number of lenght is 25")]
    public string ShortDescription { get; set; }
    [Required] 
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    public string FeaturedImageUrl { get; set; }
    [Required] 
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    [MaxLength(18, ErrorMessage = "Max number of lenght is 18")]
    public string UrlHandle { get; set; }
    [Required] 
    public DateTime PublishedDate { get; set; } 
    [Required]
    [MinLength(3, ErrorMessage = "At least 3 chars!")]
    [MaxLength(18, ErrorMessage = "Max number of lenght is 18")]
    public string Author { get; set; }
    public bool IsVisible { get; set; }
    
    // Display All created Tags
    public IEnumerable<SelectListItem>? Tags { get; set; }
    
    
    //Collected Tags
    public string[] SelectedTags { get; set; } = Array.Empty<string>();
    
}
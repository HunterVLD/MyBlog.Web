using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBlog.CustomValidators;

namespace MyBlog.Models.ViewModels.ForBlogPosts;

public class AddBlogPostRequest
{
    [Required] 
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    [MaxLength(25, ErrorMessage = "Max number of lenght is 25")]
    [NormalTextByRegex(ErrorMessage = "Invalid characters are used")]
    public string Heading { get; set; } 
    [Required] 
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    [MaxLength(25, ErrorMessage = "Max number of lenght is 25")]
    [NormalTextByRegex(ErrorMessage = "Invalid characters are used")]
    public string PageTitle { get; set; } 
    [Required] 
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    [MaxLength(4000, ErrorMessage = "Max number of lenght is 4000")]
    [NormalTextByRegex(ErrorMessage = "Invalid characters are used")]
    public string Content { get; set; }
    [Required]
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    [MaxLength(25, ErrorMessage = "Max number of lenght is 25")]
    [NormalTextByRegex(ErrorMessage = "Invalid characters are used")]
    public string ShortDescription { get; set; }
    [Required] 
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    public string FeaturedImageUrl { get; set; }
    [Required] 
    [MinLength(4, ErrorMessage = "At least 4 chars!")]
    [MaxLength(18, ErrorMessage = "Max number of lenght is 18")]
    [NormalTextByRegex(ErrorMessage = "Invalid characters are used")]
    public string UrlHandle { get; set; }
    [Required] 
    public DateTime PublishedDate { get; set; } 
    [Required]
    [MinLength(3, ErrorMessage = "At least 3 chars!")]
    [MaxLength(18, ErrorMessage = "Max number of lenght is 18")]
    [NormalTextByRegex(ErrorMessage = "Invalid characters are used")]
    public string Author { get; set; }
    public bool IsVisible { get; set; }
    
    // Display All created Tags
    public IEnumerable<SelectListItem>? Tags { get; set; }
    
    
    //Collected Tags
    public string[] SelectedTags { get; set; } = Array.Empty<string>();
    
}
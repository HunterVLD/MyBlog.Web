using System.ComponentModel.DataAnnotations;
using MyBlog.CustomValidators;
using MyBlog.Models.Domain;

namespace MyBlog.Models.ViewModels.ForBlogPosts;

public class BlogDetailsRequest
{
    public Guid Id { get; set; }
    public string Heading { get; set; } 
    public string PageTitle { get; set; } 
    public string Content { get; set; } 
    public string ShortDescription { get; set; } 
    public string FeaturedImageUrl { get; set; } 
    public string UrlHandle { get; set; } 
    
    public DateTime PublishedDate { get; set; } 
    
    public string Author { get; set; }
    public bool IsVisible { get; set; }
    
    //many to many // many TAGS relationShip
    public ICollection<Tag> Tags { get; set; }
    
    public int TotalLikes { get; set; }
    
    //for INDEX and for disabling button "like"
    public bool IsLikedByCurrentUser { get; set; }
    
    //just for your comment for write
    [Required]
    [MinLength(2, ErrorMessage = "At least 2 chars!")]
    [MaxLength(45, ErrorMessage = "Max number of lenght is 45")]
    [NormalTextByRegex(ErrorMessage = "Invalid characters are used")]
    public string CommentsDescription { get; set; }
    
    //for display all comments
    public IEnumerable<BlogComment> Comments { get; set; }
}
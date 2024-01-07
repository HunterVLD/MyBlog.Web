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
}
using Microsoft.Build.Framework;

namespace MyBlog.Models.ViewModels.ForTags;

public class AddTagRequest
{
    [Required]
    public string NameOfTag { get; set; }
    [Required]
    public string DisplayName { get; set; }
}
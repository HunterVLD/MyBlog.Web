using Microsoft.Build.Framework;

namespace MyBlog.Models.ViewModels.ForTags;

public class EditTagRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string NameOfTag { get; set; }
    [Required]
    public string DisplayName { get; set; }
}
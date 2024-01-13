using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models.ViewModels.ForTags;

public class AddTagRequest
{
    [Required]
    [MinLength(1, ErrorMessage = "At least 1 char!")]
    [MaxLength(8, ErrorMessage = "Max Tag Lenght 8 chars")]
    public string NameOfTag { get; set; }
    [Required]
    [MinLength(1, ErrorMessage = "At least 1 char!")]
    [MaxLength(8, ErrorMessage = "Max Tag Lenght 8 chars")]
    public string DisplayName { get; set; }
}
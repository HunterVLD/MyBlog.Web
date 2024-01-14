using System.ComponentModel.DataAnnotations;
using MyBlog.CustomValidators;

namespace MyBlog.Models.ViewModels.ForTags;

public class AddTagRequest
{
    [Required]
    [MinLength(1, ErrorMessage = "At least 1 char!")]
    [MaxLength(8, ErrorMessage = "Max Tag Lenght 8 chars")]
    [NormalTextByRegex(ErrorMessage = "Invalid characters are used")]
    public string NameOfTag { get; set; }
    [Required]
    [MinLength(1, ErrorMessage = "At least 1 char!")]
    [MaxLength(8, ErrorMessage = "Max Tag Lenght 8 chars")]
    [NormalTextByRegex(ErrorMessage = "Invalid characters are used")]
    public string DisplayName { get; set; }
}
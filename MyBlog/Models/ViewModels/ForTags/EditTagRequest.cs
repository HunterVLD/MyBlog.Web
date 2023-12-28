namespace MyBlog.Models.ViewModels.ForTags;

public class EditTagRequest
{
    public Guid Id { get; set; }
    public string NameOfTag { get; set; }
    public string DisplayName { get; set; }
}
using MyBlog.Models.Domain;
using MyBlog.Models.ViewModels.ForBlogPosts;

namespace MyBlog.Repositories.BlogPostRep;

public interface IBlogPostRepository
{
    Task<IEnumerable<BlogPost>> GetAllAsync();
    Task<BlogPost?> GetByIdAsync(Guid id);
    Task<BlogPost?> GetByTagAsync(Tag tag);
    //TODO PROBABLY USE RETURN BOOL
    Task<BlogPost?> AddAsync(BlogPost? post);
    Task<BlogPost?> UpdateAsync(BlogPost? post);
    //TODO PROBABLY USE RETURN BOOL
    Task<BlogPost?> DeleteAsync(Guid id);

    //Secondary functionality
    Task<BlogPost?>MappingToBlogPost(object blogPostRequest, ITagRepository tagRepository);
    Task<EditBlogPostRequest?> MappingToViewModel(BlogPost post, ITagRepository tagRepository);
}
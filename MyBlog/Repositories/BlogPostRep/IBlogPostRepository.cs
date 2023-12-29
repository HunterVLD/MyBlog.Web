using MyBlog.Models.Domain;
using MyBlog.Models.ViewModels.ForBlogPosts;

namespace MyBlog.Repositories.BlogPostRep;

public interface IBlogPostRepository
{
    Task<IEnumerable<BlogPost>> GetAllAsync();
    Task<BlogPost?> GetByIdAsync(Guid id);
    Task<BlogPost?> GetByUrlAsync(string url);
    Task<BlogPost?> GetByTagAsync(Tag tag);
    Task<BlogPost?> AddAsync(BlogPost? post);
    Task<BlogPost?> UpdateAsync(BlogPost? post);
    Task<BlogPost?> DeleteAsync(Guid id);

    //Secondary functionality
    Task<BlogPost?>MappingToBlogPost(object blogPostRequest, ITagRepository tagRepository);
    Task<EditBlogPostRequest?> MappingToViewModel(BlogPost post, ITagRepository tagRepository);
}
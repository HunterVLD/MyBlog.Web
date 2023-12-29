using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Models.Domain;
using MyBlog.Models.ViewModels.ForBlogPosts;

namespace MyBlog.Repositories.BlogPostRep;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly MyBlogDbContext _dbContext;
    
    public BlogPostRepository(MyBlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Getters
    public async Task<IEnumerable<BlogPost>> GetAllAsync()
    {
        return await _dbContext.BlogPosts.Include(x=> x.Tags).ToListAsync();
    }

    public async Task<BlogPost?> GetByIdAsync(Guid id)
    {
        return await _dbContext.BlogPosts.Include(x=> x.Tags).FirstAsync(x => x.Id == id);
    }

    public async Task<BlogPost?> GetByTagAsync(Tag tag)
    {
        throw new NotImplementedException();
    }

    public async Task<BlogPost?> GetByUrlAsync(string url)
    {
        return await _dbContext.BlogPosts.Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.UrlHandle == url);
    }
    
    #endregion
    
    #region CRUD
    public async Task<BlogPost?> AddAsync(BlogPost? post)
    {
        if (post != null) {
            await _dbContext.BlogPosts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
        }
        
        return null;
    }

    public async Task<BlogPost?> UpdateAsync(BlogPost? post)
    {
        if (post == null) {
            Console.WriteLine("Basic is null");

            return null;
        }
        
        var existingBlog = await _dbContext.BlogPosts.Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == post.Id);

        if (existingBlog != null) {
            existingBlog.Heading = post.Heading;
            existingBlog.PageTitle = post.PageTitle;
            existingBlog.Content = post.Content;
            existingBlog.ShortDescription = post.ShortDescription;
            existingBlog.FeaturedImageUrl = post.FeaturedImageUrl;
            existingBlog.UrlHandle = post.UrlHandle;
            existingBlog.PublishedDate = post.PublishedDate;
            existingBlog.Author = post.Author;
            existingBlog.IsVisible = post.IsVisible;
            existingBlog.Tags = post.Tags;

            await _dbContext.SaveChangesAsync();
            
            return existingBlog;
        }
        Console.WriteLine("existing is null");

        return null;
    }

    public async Task<BlogPost?> DeleteAsync(Guid id)
    {
        var post = await _dbContext.BlogPosts.FindAsync(id);

        if (post != null) {
            _dbContext.BlogPosts.Remove(post);
            await _dbContext.SaveChangesAsync();

            return post;
        }

        return null;
    }
    

    #endregion
    
    #region SecondaryFunctionality

    public async Task<BlogPost?> MappingToBlogPost(object blogPostRequest, ITagRepository tagRepository)
    {
        var newBlogPost = new BlogPost();
        
        if (blogPostRequest is AddBlogPostRequest addBlogPostRequest) {
            newBlogPost.Heading = addBlogPostRequest.Heading;
            newBlogPost.PageTitle = addBlogPostRequest.PageTitle;
            newBlogPost.Content = addBlogPostRequest.Content;
            newBlogPost.ShortDescription = addBlogPostRequest.ShortDescription;
            newBlogPost.FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl;
            newBlogPost.UrlHandle = addBlogPostRequest.UrlHandle;
            newBlogPost.PublishedDate = addBlogPostRequest.PublishedDate;
            newBlogPost.Author = addBlogPostRequest.Author;
            newBlogPost.IsVisible = addBlogPostRequest.IsVisible;
            newBlogPost.Tags = new List<Tag>();
        }
        else if (blogPostRequest is EditBlogPostRequest editBlogPostRequest) {
            newBlogPost.Id = editBlogPostRequest.Id;
            newBlogPost.Heading = editBlogPostRequest.Heading;
            newBlogPost.PageTitle = editBlogPostRequest.PageTitle;
            newBlogPost.Content = editBlogPostRequest.Content;
            newBlogPost.ShortDescription = editBlogPostRequest.ShortDescription;
            newBlogPost.FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl;
            newBlogPost.UrlHandle = editBlogPostRequest.UrlHandle;
            newBlogPost.PublishedDate = editBlogPostRequest.PublishedDate;
            newBlogPost.Author = editBlogPostRequest.Author;
            newBlogPost.IsVisible = editBlogPostRequest.IsVisible;
            newBlogPost.Tags = new List<Tag>();
        }
        else {
            return null;
        }
        
        //MAP Tags because we cant get obj from many-checkbox, only string value
        foreach (var selectedTagId in (blogPostRequest is AddBlogPostRequest request ? request.SelectedTags :
                     ((EditBlogPostRequest)blogPostRequest).SelectedTags))
        {
            var existingTag = await tagRepository.GetByIdAsync(Guid.Parse(selectedTagId));
            
            //MAP Tags back to BlogPost model
            if(existingTag != null)
                newBlogPost.Tags.Add(existingTag);
        }

        return newBlogPost;
    }

    public async Task<EditBlogPostRequest?> MappingToViewModel(BlogPost post, ITagRepository tagRepository)
    {
        var allTagsToView = await tagRepository.GetAllAsync();

        if (allTagsToView != null) {
            var viewModel = new EditBlogPostRequest {
                Id = post.Id,
                Heading = post.Heading,
                PageTitle = post.PageTitle,
                Content = post.Content,
                ShortDescription = post.ShortDescription,
                FeaturedImageUrl = post.FeaturedImageUrl,
                UrlHandle = post.UrlHandle,
                PublishedDate = post.PublishedDate,
                Author = post.Author,
                IsVisible = post.IsVisible,
                Tags = allTagsToView.Select(x => new SelectListItem {
                    Text = x!.DisplayName,
                    Value = x.Id.ToString()
                }),
                SelectedTags = post.Tags.Select(x=> x.Id.ToString()).ToArray()
            };

            return viewModel;
        }

        return null;
    }
    #endregion
}
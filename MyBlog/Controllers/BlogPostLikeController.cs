using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models.Domain;
using MyBlog.Models.ViewModels.ForBlogPosts;
using MyBlog.Repositories.BlogPostLikes;

namespace MyBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            _blogPostLikeRepository = blogPostLikeRepository;
        }
        
        //only in file without [FromBody]!
        [HttpPost]
        [Route("ToggleLike")]
        public async Task<IActionResult> ToggleLike([FromBody] AddLikeRequest addLikeRequest)
        {
            /*Console.WriteLine($"=================================================================NIGGA\n" +
                              $"\n" +
                              $"{addLikeRequest} -BASIS | {addLikeRequest.IsLikedThisPost} / {addLikeRequest.BlogPostId} / {addLikeRequest.UserId}\n" +
                              $"=================================================================NIGGA");*/
            if (addLikeRequest.IsLikedThisPost == 1) {
                var currentLike =
                    await _blogPostLikeRepository.GetUserLikeIdForCurrentPostAsync(addLikeRequest.BlogPostId,
                        addLikeRequest.UserId);

                if (currentLike == null)
                    return BadRequest();

                var res = await _blogPostLikeRepository.DeleteLikeForBlogAsync(currentLike.Id);

                if (!res)
                    return BadRequest();
                
                return Ok();
            }
            
            var like = new BlogPostLike {
                BlogPostId = addLikeRequest.BlogPostId,
                UserId = addLikeRequest.UserId
            };
            
            //we create new LIKE MODEL, because we wanna generate id automaticaly
            await _blogPostLikeRepository.AddLikeForBlogAsync(like);

            return Ok();
        }

        [HttpGet]
        [Route("{blogPostId:guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
        {
            var allBlogPostLikes = await _blogPostLikeRepository.GetTotalLikesAsync(blogPostId);

            return Ok(allBlogPostLikes);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

using PhotoAlbumBLL.Interfaces;
using PhotoAlbumBLL.DTO;

namespace WebPhotoAlbum.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        public IPostService PostService { get; }
        public ITagService TagService { get; }
        public IConfiguration Configuration { get; }

        public TagController(ITagService service, IPostService postService, IConfiguration configuration)
        {
            PostService = postService;
            TagService = service;
            Configuration = configuration;
        }

        /// <summary>
        /// METHOD: GET;
        /// ROUTE: api/tag/<string>/posts;
        /// HEADER: jwt-token;
        /// </summary>
        /// <param name="name">Unique name of tag</param>
        /// <returns>Posts with tag</returns>
        [HttpGet("{name}/posts")]
        public async Task<IActionResult> GetAllPostWithTag(string name)
        {
            try
            {
                SearchTagDTO searchTag = await TagService.GetTagByName(name);
                IEnumerable<PostDTO> postsToReturn = await TagService.GetAllPostsByTag(searchTag);

                if (postsToReturn.Count() == 0)
                    return StatusCode(204);

                return Ok(postsToReturn);
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }

        /// <summary>
        /// METHOD: GET;
        /// ROUTE: api/tag/<string>/posts/<int>-<int>;
        /// HEADER: jwt-token;
        /// </summary>
        /// <param name="name">Unique name of tag</param>
        /// <param name="from">Begin index of post (NOT AN ACTUAL ID OF POST IN DATABASE)</param>
        /// <param name="to">End index of post (NOT AN ACTUAL ID OF POST IN DATABASE)</param>
        /// <returns>Posts with tag</returns>
        [HttpGet("{name}/posts/{from}-{to}")]
        public async Task<IActionResult> GetPostRangeWithTag(string name, int from, int to)
        {
            try
            {
                SearchTagDTO searchTag = await TagService.GetTagByName(name);
                IEnumerable<PostDTO> postsToReturn = await TagService.GetPostsRangeByTag(searchTag, from, to);

                if (postsToReturn.Count() == 0)
                    return StatusCode(204);

                return Ok(postsToReturn);
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }

        /// <summary>
        /// METHOD: POST;
        /// ROUTE: api/tag
        /// HEADER: jwt-token;
        /// FORM:
        ///     - tagName: string
        /// </summary>
        /// <param name="tagName">Name of new tag</param>
        /// <returns>Status codes</returns>
        [HttpPost]
        public async Task<IActionResult> AddNewTag([FromForm] string tagName)
        {
            try
            {
                await TagService.AddTag(new SearchTagDTO
                {
                    Name = tagName.ToLower()
                });

                return Ok("Tag was successfully added!");
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }
    }
}

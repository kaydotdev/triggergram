using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
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
    public class PostsController : ControllerBase
    {
        public ITagService TagService { get; }
        public IPhotoService PhotoService { get; }
        public IPostService PostService { get; }
        public IConfiguration Configuration { get; }

        public PostsController(IPostService service, 
            IPhotoService photoService, 
            ITagService tagService,
            IConfiguration configuration)
        {
            TagService = tagService;
            PhotoService = photoService;
            PostService = service;
            Configuration = configuration;
        }

        /// <summary>
        /// METHOD: GET;
        /// ROUTE: api/posts;
        /// HEADER: jwt-token;
        /// </summary>
        /// <returns>All posts of user (user data stored in claims in token)</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUserPosts()
        {
            string username = User.Identity.Name;
            try
            {
                IEnumerable<PostDTO> result = await PostService.GetAllUserPosts(new UserDTO { UserName = username });

                if (result.Count() == 0)
                    return StatusCode(204);
                return Ok(result);
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }

        /// <summary>
        /// METHOD: GET;
        /// ROUTE: api/posts/<int>-<int>;
        /// HEADER: jwt-token;
        /// </summary>
        /// <param name="from">Begin index of posts</param>
        /// <param name="to">End index of posts</param>
        /// <returns>Range of posts between begin index and end index</returns>
        [HttpGet("{from}-{to}")]
        public async Task<IActionResult> GetUserPostsRange(int from, int to)
        {
            string username = User.Identity.Name;
            try
            {
                IEnumerable<PostDTO> result = await PostService.GetUserPostsRange(new UserDTO { UserName = username }, from, to);

                if (result.Count() == 0)
                    return StatusCode(204);
                else
                    return Ok(result);
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }

        /// <summary>
        /// METHOD: GET;
        /// ROUTE: api/posts/<int>/photo
        /// HEADER: jwt-token;
        /// </summary>
        /// <param name="id">ID of post (ACTUAL ID FROM DATABASE)</param>
        /// <returns>Photo of the post</returns>
        [HttpGet("{id}/photo")]
        public async Task<IActionResult> GetPostPhoto(int id)
        {
            string username = User.Identity.Name;
            try
            {
                PhotoDTO photo = await PhotoService.GetPhotoOfPost(new PostDTO { Id = id });

                if (photo == null)
                    return StatusCode(204);
                else
                    return Ok(photo);
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }

        /// <summary>
        /// METHOD: GET;
        /// ROUTE: api/posts/<int>/tags
        /// HEADER: jwt-token;
        /// </summary>
        /// <param name="id">ID of post (ACTUAL ID FROM DATABASE)</param>
        /// <returns>Search tags of post</returns>
        [HttpGet("{id}/tags")]
        public async Task<IActionResult> GetAllTagsOfPost(int id)
        {
            try
            {
                IEnumerable<SearchTagDTO> result = await TagService.GetPostTags(new PostDTO { Id = id });

                if (result.Count() == 0)
                    return StatusCode(204);
                return Ok(result);
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }

        /// <summary>
        /// METHOD: POST;
        /// ROUTE: api/posts;
        /// HEADER: jwt-token;
        /// FORM:
        ///     - photoFile: File
        ///     - postDescription: string
        /// </summary>
        /// <param name="photoFile">Photo file from form</param>
        /// <param name="postDescription">Description of post from form</param>
        /// <returns>Status code</returns>
        [HttpPost]
        public async Task<IActionResult> AddNewPost([FromForm] IFormFile photoFile, [FromForm] string postDescription)
        {
            string username = User.Identity.Name;

            if (photoFile.Length > 0)
            {
                byte[] photoToUpload = new byte[photoFile.Length];

                using (MemoryStream ms = new MemoryStream())
                {
                    await photoFile.CopyToAsync(ms);
                    photoToUpload = ms.ToArray();
                }

                PhotoDTO photoToSave = new PhotoDTO { Name = photoFile.FileName, Source = photoToUpload };
                PostDTO postToCreate = new PostDTO { Description = postDescription, PostingDate = DateTime.Now };
                UserDTO user = new UserDTO { UserName = username };

                try
                {
                    await PostService.AddPost(photoToSave, postToCreate, user);
                    return Ok("Picture was successfully added!");
                }
                catch (ArgumentException ex)
                { return BadRequest(ex.Message); }
                catch (Exception ex)
                { return StatusCode(500, "Oops, something went wrong!"); }
             }
             else
                return BadRequest("Uploaded photo is invalid!");
        }

        /// <summary>
        /// METHOD: PUT;
        /// ROUTE: api/Posts/<int>/photo
        /// HEADER: jwt-token;
        /// FORM:
        ///     - photoFile: File
        /// </summary>
        /// <param name="id">Index of user's post (NOT ACTUAL ID OF POST IN DATABASE!)</param>
        /// <param name="photoFile">Photo file from form</param>
        /// <returns>Status codes</returns>
        [HttpPut("{id}/photo")]
        public async Task<IActionResult> ChangePhoto(int id, [FromForm] IFormFile photoFile)
        {
            string username = User.Identity.Name;
            try
            {
                IEnumerable<PostDTO> posts = await PostService.GetUserPostsRange(new UserDTO { UserName = username }, id, id + 1);
                PostDTO postToChange = posts.FirstOrDefault();

                if (postToChange == null)
                    return Unauthorized();

                if (photoFile.Length > 0)
                {
                    byte[] photoToUpload = new byte[photoFile.Length];

                    using (MemoryStream ms = new MemoryStream())
                    {
                        await photoFile.CopyToAsync(ms);
                        photoToUpload = ms.ToArray();
                    }

                    PhotoDTO photoToChange = new PhotoDTO { Name = photoFile.FileName, Source = photoToUpload };

                    await PostService.ChangePhotoOnPost(photoToChange, postToChange);
                    return Ok("Photo was changed successfully!");
                }
                else
                    return BadRequest("Uploaded photo is invalid!");
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }

        /// <summary>
        /// METHOD: PUT;
        /// ROUTE: api/Posts/<int>/description
        /// HEADER: jwt-token;
        /// FORM:
        ///     - description: string
        /// </summary>
        /// <param name="id">Index of user's post (NOT ACTUAL ID OF POST IN DATABASE!)</param>
        /// <param name="description">New description string from form</param>
        /// <returns></returns>
        [HttpPut("{id}/description")]
        public async Task<IActionResult> ChangeDescription(int id, [FromForm] string description)
        {
            string username = User.Identity.Name;
            try
            {
                IEnumerable<PostDTO> posts = await PostService.GetUserPostsRange(new UserDTO { UserName = username }, id, id + 1);
                PostDTO postToChange = posts.FirstOrDefault();

                if (postToChange == null)
                    return Unauthorized();

                await PostService.EditDescription(new PostDTO {
                    Id = postToChange.Id,
                    Description = description,
                    PostingDate = postToChange.PostingDate
                });

                return Ok("Description changed successfully!");
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }

        /// <summary>
        /// METHOD: PUT;
        /// ROUTE: api/posts/<int>/tag
        /// HEADER: jwt-token;
        /// </summary>
        /// <param name="id">Index of user post (NOT ACTUAL ID IN DATABASE!)</param>
        /// <param name="tagName">Unique name for tag to attach</param>
        /// <returns>Status codes</returns>
        [HttpPut("{id}/tag")]
        public async Task<IActionResult> AttachTagToThePost(int id, [FromForm] string tagName)
        {
            string username = User.Identity.Name;
            try
            {
                IEnumerable<PostDTO> posts = await PostService.GetUserPostsRange(new UserDTO { UserName = username }, id, id + 1);
                PostDTO postToChange = posts.FirstOrDefault();

                SearchTagDTO tagToAttach = await TagService.GetTagByName(tagName);

                if (postToChange == null || tagToAttach == null)
                    return Unauthorized();

                await TagService.PutTagOnPost(tagToAttach, postToChange);
                return Ok("Description changed successfully!");
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }

        /// <summary>
        /// METHOD: DELETE;
        /// ROUTE: api/posts/<int>/tag
        /// HEADER: jwt-token;
        /// </summary>
        /// <param name="id">Index of user's post (NOT ACTUAL ID OF POST IN DATABASE!)</param>
        /// /// <param name="tagName">Unique name for tag to detach</param>
        /// <returns>Status codes</returns>
        [HttpDelete("{id}/tag")]
        public async Task<IActionResult> DetachTagToThePost(int id, [FromForm] string tagName)
        {
            string username = User.Identity.Name;
            try
            {
                IEnumerable<PostDTO> posts = await PostService.GetUserPostsRange(new UserDTO { UserName = username }, id, id + 1);
                PostDTO postToChange = posts.FirstOrDefault();

                SearchTagDTO tagToAttach = await TagService.GetTagByName(tagName);

                if (postToChange == null || tagToAttach == null)
                    return Unauthorized();

                await TagService.DetachTagFromPost(tagToAttach, postToChange);
                return Ok("Description changed successfully!");
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string username = User.Identity.Name;
            try
            {
                IEnumerable<PostDTO> posts = await PostService.GetUserPostsRange(new UserDTO { UserName = username }, id, id + 1);

                if (posts.FirstOrDefault() == null)
                    return Unauthorized();

                await PostService.DeletePost(posts.FirstOrDefault());
                return Ok("Post was successfully deleted!");
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }
    }
}

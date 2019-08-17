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
        public IPostService PostService { get; }
        public IConfiguration Configuration { get; }

        public PostsController(IPostService service, IConfiguration configuration)
        {
            PostService = service;
            Configuration = configuration;
        }

        /// <summary>
        /// METHOD: GET
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
                    return BadRequest("Post with this ID doesn't exist!");

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
                    return BadRequest("Post with this ID doesn't exist!");

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
        /// METHOD: DELETE;
        /// ROUTE: api/Posts/<int>
        /// HEADER: jwt-token;
        /// </summary>
        /// <param name="id">Index of user's post (NOT ACTUAL ID OF POST IN DATABASE!)</param>
        /// <returns>Status codes</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string username = User.Identity.Name;
            try
            {
                IEnumerable<PostDTO> posts = await PostService.GetUserPostsRange(new UserDTO { UserName = username }, id, id + 1);

                if (posts.FirstOrDefault() == null)
                    return BadRequest("Post with this ID doesn't exist");

                await PostService.DeletePost(posts.FirstOrDefault());
                return Ok();
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }
    }
}

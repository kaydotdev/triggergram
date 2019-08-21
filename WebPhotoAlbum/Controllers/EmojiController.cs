using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

using PhotoAlbumBLL.Interfaces;
using PhotoAlbumBLL.DTO;

namespace WebPhotoAlbum.Controllers
{
    [Authorize(Roles = "3")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmojiController : ControllerBase
    {
        public IEmojiService EmojiService { get; set; }
        public EmojiController(IEmojiService emojiService)
        {
            EmojiService = emojiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmojies()
        {
            try
            {
                IEnumerable<EmojiDTO> emojis = await EmojiService.GetAllEmojies();
                return Ok(emojis);
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }

        /// <summary>
        /// METHOD: POST;
        /// ROUTE: api/emoji;
        /// HEADER: jwt-token (with claim level 3);
        /// FORM:
        ///     - name: string
        ///     - emojiFile: File
        /// </summary>
        /// <param name="name"></param>
        /// <param name="emojiFile"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddEmoji([FromForm] string name, [FromForm] IFormFile emojiFile)
        {
            try
            {
                if (emojiFile.Length > 0)
                {
                    byte[] photoToUpload = new byte[emojiFile.Length];

                    using (MemoryStream ms = new MemoryStream())
                    {
                        await emojiFile.CopyToAsync(ms);
                        photoToUpload = ms.ToArray();
                    }

                    EmojiDTO newEmoji = new EmojiDTO { Name = name, Source = photoToUpload };

                    await EmojiService.AddEmoji(newEmoji);
                    return Ok("New emoji was added successfully!");
                }
                else
                    return BadRequest("Uploaded emoji picture file is invalid!");
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteEmoji([FromForm] string name)
        {
            try
            {
                await EmojiService.RemoveEmoji(new EmojiDTO { Name = name });
                return Ok("New emoji was added successfully!");
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }
    }
}

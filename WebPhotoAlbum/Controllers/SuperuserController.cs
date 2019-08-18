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
    [Authorize(Roles = "3")]
    [Route("api/[controller]")]
    [ApiController]
    public class SuperuserController : ControllerBase
    {
        public IUserService UserService { get; set; }

        public SuperuserController(IUserService userService)
        {
            UserService = userService;
        }

        /// <summary>
        /// METHOD: PUT;
        /// ROUTE: api/superuser/<string>;
        /// HEADER: jwt-token (with claim level 3);
        /// BODY:
        ///     {
        ///         "id": <int>
        ///         "name": <string>
        ///     }
        /// </summary>
        /// <param name="username">Unique name of user</param>
        /// <param name="role">Role DTO that user will be promoted</param>
        /// <returns>Status codes</returns>
        [HttpPut("{username}")]
        public async Task<IActionResult> PromoteUser(string username, [FromBody] UserRoleDTO role)
        {
            try
            {
                await UserService.PromoteUser(new UserDTO { UserName = username }, role);
                return Ok($"User was promoted to {role.Name}!");
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }

        /// <summary>
        /// METHOD: DELETE;
        /// ROUTE: api/superuser/<string>;
        /// HEADER: jwt-token (with claim level 3);
        /// </summary>
        /// <param name="username">Unique name of user</param>
        /// <returns>Status codes</returns>
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            try
            {
                await UserService.DeleteUser(new UserDTO { UserName = username });
                return Ok($"User '{username}' was deleted!");
            }
            catch (ArgumentException ex)
            { return BadRequest(ex.Message); }
            catch (Exception ex)
            { return StatusCode(500, "Oops, something went wrong!"); }
        }
    }
}

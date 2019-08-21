using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

using PhotoAlbumBLL.Interfaces;
using PhotoAlbumBLL.DTO;

namespace WebPhotoAlbum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService AuthService { get; }
        public IConfiguration Configuration { get; }

        public AuthController(IAuthService service, IConfiguration configuration)
        {
            AuthService = service;
            Configuration = configuration;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(NewUserDTO newUser)
        {
            if (await AuthService.UserExists(newUser))
                return BadRequest("User with this nickname already exists!");

            await AuthService.SignUp(newUser);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO user)
        {
            ClaimedUserDTO resultUser = await AuthService.Login(user);

            if (resultUser == null)
                return BadRequest("Wrong username or password!");

            Claim[] claims = new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, resultUser.UserId.ToString()),
                new Claim(ClaimTypes.Name, resultUser.UserName.ToString()),
                new Claim(ClaimTypes.Role, resultUser.RoleId.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(5),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken token = handler.CreateToken(descriptor);

            Response.Cookies.Append("token", handler.WriteToken(token).ToString());
            return Ok(new {
                token = handler.WriteToken(token)
            });
        }

    }
}
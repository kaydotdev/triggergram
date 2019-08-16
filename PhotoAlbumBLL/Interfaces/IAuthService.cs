using System;
using System.Threading.Tasks;

using PhotoAlbumBLL.DTO;


namespace PhotoAlbumBLL.Interfaces
{
    public interface IAuthService : IDisposable
    {
        Task SignUp(NewUserDTO user);
        Task<ClaimedUserDTO> Login(UserDTO user);
        Task<bool> UserExists(NewUserDTO user);
    }
}

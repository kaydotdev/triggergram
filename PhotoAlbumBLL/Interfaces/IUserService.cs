using System;
using System.Threading.Tasks;

using PhotoAlbumBLL.DTO;


namespace PhotoAlbumBLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task PromoteUser(UserDTO user, UserRoleDTO role);
        Task DeleteUser(UserDTO user);
    }
}

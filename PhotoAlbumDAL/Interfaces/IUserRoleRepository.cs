using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Interfaces
{
    public interface IUserRoleRepository : IRepository<UserRole, int>, IAsyncRepository<UserRole, int> { }
}

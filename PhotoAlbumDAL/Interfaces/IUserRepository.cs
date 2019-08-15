using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Interfaces
{
    public interface IUserRepository : IRepository<User, int>, IAsyncRepository<User, int> { }
}

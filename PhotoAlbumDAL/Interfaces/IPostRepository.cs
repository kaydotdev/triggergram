using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Interfaces
{
    public interface IPostRepository : IRepository<PhotoPost, int>, IAsyncRepository<PhotoPost, int> { }
}

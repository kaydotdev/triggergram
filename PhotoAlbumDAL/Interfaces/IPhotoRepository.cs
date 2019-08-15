using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Interfaces
{
    public interface IPhotoRepository : IRepository<Photo, int>, IAsyncRepository<Photo, int> { }
}

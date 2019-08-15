using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Interfaces
{
    public interface ICommentRepository : IRepository<PhotoPostComment, int>, IAsyncRepository<PhotoPostComment, int> { }
}

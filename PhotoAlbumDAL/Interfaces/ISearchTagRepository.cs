using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Interfaces
{
    public interface ISearchTagRepository : IRepository<SearchTag, int>, IAsyncRepository<SearchTag, int> { }
}

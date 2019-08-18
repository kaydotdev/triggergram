using System;
using System.Threading.Tasks;

using PhotoAlbumBLL.DTO;

namespace PhotoAlbumBLL.Interfaces
{
    public interface IPhotoService : IDisposable
    {
        Task<PhotoDTO> GetPhotoOfPost(PostDTO post);
    }
}

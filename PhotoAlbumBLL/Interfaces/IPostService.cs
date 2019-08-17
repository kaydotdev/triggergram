using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PhotoAlbumBLL.DTO;

namespace PhotoAlbumBLL.Interfaces
{
    public interface IPostService : IDisposable
    {
        Task AddPost(PhotoDTO photo, PostDTO post, UserDTO user);
        Task DeletePost(PostDTO post);
        Task<IEnumerable<PostDTO>> GetAllUserPosts(UserDTO user);
        Task<IEnumerable<PostDTO>> GetUserPostsRange(UserDTO user, int from, int to);
        Task EditDescription(PostDTO post);
        Task ChangePhotoOnPost(PhotoDTO photo, PostDTO post);
    }
}

using System;
using System.Threading.Tasks;

using PhotoAlbumDAL.Models;
using PhotoAlbumDAL.Interfaces;

using PhotoAlbumBLL.Interfaces;
using PhotoAlbumBLL.DTO;

namespace PhotoAlbumBLL.Services
{
    public class PhotoService : IPhotoService
    {
        private IUnitOfWork _dbcontext;

        public PhotoService(IUnitOfWork context) { _dbcontext = context; }

        public async Task<PhotoDTO> GetPhotoOfPost(PostDTO post)
        {
            if (post == null)
                throw new ArgumentException("Post data is empty!");

            PhotoPost seekedPost = await _dbcontext.Posts.GetByKeyAsync(post.Id);
            Photo photoOfPost = seekedPost.PhotoNav;

            if (photoOfPost == null)
                return null;
            else
                return new PhotoDTO {
                    Id = photoOfPost.Id,
                    Name = photoOfPost.Name,
                    Source = photoOfPost.Source
                };
        }

        public void Dispose() { _dbcontext.Dispose(); }
    }
}

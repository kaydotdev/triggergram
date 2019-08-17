using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;


using PhotoAlbumDAL.Models;
using PhotoAlbumDAL.Interfaces;

using PhotoAlbumBLL.Interfaces;
using PhotoAlbumBLL.DTO;

namespace PhotoAlbumBLL.Services
{
    public class PostService : IPostService
    {
        private IUnitOfWork _dbcontext;

        public PostService(IUnitOfWork context) { _dbcontext = context; }

        public async Task AddPost(PhotoDTO photo, PostDTO post, UserDTO user)
        {
            if (photo.Name == null || photo.Source == null)
                throw new ArgumentException("Photo data is not complete!");

            Photo newPhoto = new Photo {
                Name = photo.Name,
                Source = photo.Source
            };

            await _dbcontext.Photos.CreateAsync(newPhoto);

            IEnumerable<User> seekedUser = await _dbcontext.Users.GetByConditionAsync(u => u.Nickname == user.UserName);

            if (seekedUser.FirstOrDefault() == null)
                throw new ArgumentException("This user doesn't exist!");

            PhotoPost newPhotoPost = new PhotoPost {
                Description = post.Description,
                PostingDate = post.PostingDate,
                PhotoNav = newPhoto,
                UserNav = seekedUser.FirstOrDefault()
            };

            await _dbcontext.Posts.CreateAsync(newPhotoPost);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task ChangePhotoOnPost(PhotoDTO photo, PostDTO post)
        {
            PhotoPost postToChange = await _dbcontext.Posts.GetByKeyAsync(post.Id);

            if (postToChange == null)
                throw new ArgumentException("Post with this ID doesn't exist!");

            if (postToChange.PhotoNav != null)
            {
                Photo postPhoto = postToChange.PhotoNav;
                postPhoto.Name = photo.Name;
                postPhoto.Source = photo.Source;
            }
            else
            {
                Photo newPhoto = new Photo {
                    Name = photo.Name,
                    Source = photo.Source
                };

                await _dbcontext.Photos.CreateAsync(newPhoto);
                postToChange.PhotoNav = newPhoto;
            }

            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeletePost(PostDTO post)
        {
            PhotoPost postToDelete = await _dbcontext.Posts.GetByKeyAsync(post.Id);

            if (postToDelete == null)
                throw new ArgumentException("Post with this ID doesn't exist!");

            _dbcontext.Photos.Delete(postToDelete.PhotoNav);
            _dbcontext.Posts.Delete(postToDelete);

            await _dbcontext.SaveChangesAsync();
        }

        public void Dispose() { _dbcontext.Dispose(); }

        public async Task EditDescription(PostDTO post)
        {
            PhotoPost seekedPost = await _dbcontext.Posts.GetByKeyAsync(post.Id);

            if (seekedPost != null)
                seekedPost.Description = post.Description;

            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostDTO>> GetAllUserPosts(UserDTO user)
        {
            Queue<PostDTO> resultPosts = new Queue<PostDTO>();
            IEnumerable<User> seekedUser = await _dbcontext.Users.GetByConditionAsync(u => u.Nickname == user.UserName);

            if (seekedUser.FirstOrDefault() == null)
                throw new ArgumentException("This user doesn't exist!");

            IEnumerable<PhotoPost> usersPosts = seekedUser
                .FirstOrDefault()
                .PhotoPosts
                .OrderByDescending(p => p.PostingDate);

            foreach (var post in usersPosts)
            {
                resultPosts.Enqueue(new PostDTO {
                    Id = post.Id,
                    Description = post.Description,
                    PostingDate = post.PostingDate
                });
            }

            return resultPosts;
        }

        public async Task<IEnumerable<PostDTO>> GetUserPostsRange(UserDTO user, int from, int to)
        {
            if (from >= to || from < 0)
                throw new ArgumentException("Wrong range order!");

            Queue<PostDTO> resultPosts = new Queue<PostDTO>();
            IEnumerable<User> seekedUser = await _dbcontext.Users.GetByConditionAsync(u => u.Nickname == user.UserName);

            if (seekedUser.FirstOrDefault() == null)
                throw new ArgumentException("This user doesn't exist!");

            IEnumerable<PhotoPost> usersPosts = seekedUser
                .FirstOrDefault()
                .PhotoPosts
                .OrderByDescending(p => p.PostingDate)
                .Skip(from)
                .Take(to - from);

            foreach (var post in usersPosts)
            {
                resultPosts.Enqueue(new PostDTO
                {
                    Id = post.Id,
                    Description = post.Description,
                    PostingDate = post.PostingDate
                });
            }

            return resultPosts;
        }
    }
}

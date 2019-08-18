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
    public class CommentService : ICommentService
    {
        private IUnitOfWork _dbcontext;
        public CommentService(IUnitOfWork context) { _dbcontext = context; }

        public async Task AddComment(CommentDTO comment, PostDTO post, UserDTO user)
        {
            if (string.IsNullOrEmpty(comment?.Content))
                throw new ArgumentException("Context of comment cannot be empty!");

            PhotoPost postToComment = await _dbcontext.Posts.GetByKeyAsync(post.Id);
            IEnumerable<User> users = await _dbcontext.Users.GetByConditionAsync(u => u.Nickname == user.UserName);
            User UserThatCommenting = users.FirstOrDefault();

            PhotoPostComment commentToCreate = new PhotoPostComment {
                Content = comment.Content,
                PhotoPostId = postToComment.Id,
                UserId = UserThatCommenting.Id
            };

            await _dbcontext.Comments.CreateAsync(commentToCreate);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task EditComment(CommentDTO comment)
        {
            if (string.IsNullOrEmpty(comment?.Content))
                throw new ArgumentException("Context of comment cannot be empty!");

            PhotoPostComment commentToModify = _dbcontext.Comments.GetByKey(comment.Id);

            if (commentToModify != null)
                commentToModify.Content = comment.Content;

            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommentDTO>> GetAllPostsComments(PostDTO post)
        {
            Queue<CommentDTO> resultComments = new Queue<CommentDTO>();
            PhotoPost seekedPost = await _dbcontext.Posts.GetByKeyAsync(post.Id);

            if (seekedPost == null)
                return null;

            IEnumerable <PhotoPostComment> postComment = seekedPost
                .PostsComments
                .OrderByDescending(p => p.Id);

            foreach (var comment in postComment)
                resultComments.Enqueue(new CommentDTO {
                    Id = comment.Id,
                    Content = comment.Content
                });

            return resultComments;
        }

        public async Task<IEnumerable<CommentDTO>> GetPostCommentsRange(PostDTO post, int from, int to)
        {
            Queue<CommentDTO> resultComments = new Queue<CommentDTO>();
            PhotoPost seekedPost = await _dbcontext.Posts.GetByKeyAsync(post.Id);

            if (seekedPost == null)
                return null;

            IEnumerable<PhotoPostComment> postComment = seekedPost
                .PostsComments
                .OrderByDescending(p => p.Id)
                .Skip(from)
                .Take(to);

            foreach (var comment in postComment)
                resultComments.Enqueue(new CommentDTO
                {
                    Id = comment.Id,
                    Content = comment.Content
                });

            return resultComments;
        }

        public async Task RemoveComment(CommentDTO comment)
        {
            PhotoPostComment commentToDelete = _dbcontext.Comments.GetByKey(comment.Id);

            if (commentToDelete != null)
                _dbcontext.Comments.Delete(commentToDelete);

            await _dbcontext.SaveChangesAsync();
        }

        public void Dispose() { _dbcontext.Dispose(); }
    }
}

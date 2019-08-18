using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PhotoAlbumBLL.DTO;

namespace PhotoAlbumBLL.Interfaces
{
    public interface ICommentService : IDisposable
    {
        Task AddComment(CommentDTO comment, PostDTO post, UserDTO user);
        Task RemoveComment(CommentDTO comment);
        Task EditComment(CommentDTO comment);
        Task<IEnumerable<CommentDTO>> GetAllPostsComments(PostDTO post);
        Task<IEnumerable<CommentDTO>> GetPostCommentsRange(PostDTO post, int from, int to);
    }
}

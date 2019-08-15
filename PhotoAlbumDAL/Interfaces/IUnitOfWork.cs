using System;
using System.Threading.Tasks;

namespace PhotoAlbumDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICommentRepository Comments { get; }
        IEmojiRepository Emojis { get; }
        IPhotoRepository Photos { get; }
        IPostRepository Posts { get; }
        ISearchTagRepository SearchTags { get; }
        IUserRepository Users { get; }
        IUserRoleRepository UserRoles { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}

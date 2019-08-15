using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Interfaces
{
    public interface IEmojiRepository: IRepository<EmojiMark, string>, IAsyncRepository<EmojiMark, string> { }
}

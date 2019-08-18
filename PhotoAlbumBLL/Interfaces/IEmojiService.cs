using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PhotoAlbumBLL.DTO;

namespace PhotoAlbumBLL.Interfaces
{
    public interface IEmojiService : IDisposable
    {
        Task AddEmoji(EmojiDTO emoji);
        Task RemoveEmoji(EmojiDTO emoji);
        Task<IEnumerable<EmojiDTO>> GetAllEmojies();
        Task<IEnumerable<EmojiDTO>> GetAllEmojiesFromPost(PostDTO post);
        Task<IEnumerable<GroupedEmojiDTO>> GetAmmoutOfEmojiesOnPost(PostDTO post);
        Task MarkEmojiToPost(EmojiDTO emoji, PostDTO post, UserDTO user);
        Task RemoveEmojiToPost(EmojiDTO emoji, PostDTO post, UserDTO user);
    }
}

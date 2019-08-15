using System.Collections.Generic;

namespace PhotoAlbumDAL.Models
{
    public class EmojiMark
    {
        public string Name { get; set; }
        public byte[] Source { get; set; }

        public ICollection<PostsEmojiMark> PostsEmojiMarks { get; set; }
    }
}

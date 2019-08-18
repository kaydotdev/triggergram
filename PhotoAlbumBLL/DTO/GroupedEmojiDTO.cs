using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoAlbumBLL.DTO
{
    public class GroupedEmojiDTO
    {
        public EmojiDTO Emoji { get; set; }
        public int Count { get; set; }
    }
}

namespace PhotoAlbumDAL.Models
{
    public class PostsEmojiMark
    {
        public string EmojiName { get; set; }
        public EmojiMark EmojiMarkNav { get; set; }

        public int PhotoPostId { get; set; }
        public PhotoPost PhotoPostNav { get; set; }
    }
}

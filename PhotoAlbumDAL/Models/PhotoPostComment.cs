namespace PhotoAlbumDAL.Models
{
    public class PhotoPostComment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int PhotoPostId { get; set; }
        public PhotoPost PhotoPostNav { get; set; }

        public int? UserId { get; set; }
        public User UserNav { get; set; }
    }
}

namespace PhotoAlbumDAL.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Source { get; set; }

        public PhotoPost PhotoPostNav { get; set; }
    }
}

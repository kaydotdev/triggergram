namespace PhotoAlbumBLL.Security
{
    public struct EncryptedPassword
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}

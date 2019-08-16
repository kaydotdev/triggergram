using System.ComponentModel.DataAnnotations;

namespace PhotoAlbumBLL.DTO
{
    public class ClaimedUserDTO
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int? RoleId { get; set; }
    }
}

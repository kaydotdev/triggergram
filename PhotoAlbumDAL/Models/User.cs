using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoAlbumDAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public int? RoleId { get; set; }
        public UserRole UserRoleNav { get; set; }

        public ICollection<PhotoPost> PhotoPosts { get; set; }
        public ICollection<PhotoPostComment> PhotoPostComments { get; set; }
    }
}

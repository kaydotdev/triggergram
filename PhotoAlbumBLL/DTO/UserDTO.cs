using System.ComponentModel.DataAnnotations;

namespace PhotoAlbumBLL.DTO
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Username field cannot be empty!")]
        [StringLength(50, ErrorMessage = "Username field is too big! Max length is 50 characters!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password field cannot be empty!")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password field length should be between 6 and 100 characters!")]
        public string Password { get; set; }
    }
}

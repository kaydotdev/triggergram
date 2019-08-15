using System;
using System.Collections.Generic;

namespace PhotoAlbumDAL.Models
{
    public class PhotoPost
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime PostingDate { get; set; }

        public int UserId { get; set; }
        public User UserNav { get; set; }

        public int? PhotoId { get; set; }
        public Photo PhotoNav { get; set; }

        public ICollection<PostsEmojiMark> PostsEmojiMarks { get; set; }
        public ICollection<PostsSearchTag> PostsSearchTags { get; set; }
        public ICollection<PhotoPostComment> PostsComments { get; set; }
    }
}

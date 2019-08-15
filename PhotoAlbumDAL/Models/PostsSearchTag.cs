using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoAlbumDAL.Models
{
    public class PostsSearchTag
    {
        public int PhotoPostId { get; set; }
        public PhotoPost PhotoPostNav { get; set; }

        public int SearchTagId { get; set; }
        public SearchTag SearchTagNav { get; set; }
    }
}

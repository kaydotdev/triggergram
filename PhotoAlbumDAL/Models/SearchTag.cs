using System.Collections.Generic;

namespace PhotoAlbumDAL.Models
{
    public class SearchTag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<PostsSearchTag> PostsSearchTags { get; set; }
    }
}

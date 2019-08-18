using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PhotoAlbumBLL.DTO;

namespace PhotoAlbumBLL.Interfaces
{
    public interface ITagService : IDisposable
    {
        Task AddTag(SearchTagDTO tag);
        Task RemoveTag(SearchTagDTO tag);
        Task PutTagOnPost(SearchTagDTO tag, PostDTO post);
        Task DetachTagFromPost(SearchTagDTO tag, PostDTO post);
        Task<IEnumerable<PostDTO>> GetAllPostsByTag(SearchTagDTO tag);
        Task<IEnumerable<PostDTO>> GetPostsRangeByTag(SearchTagDTO tag, int from, int to);
        Task<IEnumerable<SearchTagDTO>> GetPostTags(PostDTO post);
        Task<SearchTagDTO> GetTagByName(string name);
    }
}

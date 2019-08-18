using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;


using PhotoAlbumDAL.Models;
using PhotoAlbumDAL.Interfaces;

using PhotoAlbumBLL.Interfaces;
using PhotoAlbumBLL.DTO;

namespace PhotoAlbumBLL.Services
{
    public class TagService : ITagService
    {
        private IUnitOfWork _dbcontext;

        public TagService(IUnitOfWork context) { _dbcontext = context; }

        public async Task AddTag(SearchTagDTO tag)
        {
            if (string.IsNullOrEmpty(tag.Name))
                throw new ArgumentException("Tag name cannot be empty!");


            if (_dbcontext.SearchTags.GetByCondition(t => t.Name == tag.Name).FirstOrDefault() == null)
            {
                await _dbcontext.SearchTags.CreateAsync(new SearchTag { Name = tag.Name });
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task PutTagOnPost(SearchTagDTO tag, PostDTO post)
        {
            PhotoPost postToChange = await _dbcontext.Posts.GetByKeyAsync(post.Id);
            SearchTag tagToAttach = await _dbcontext.SearchTags.GetByKeyAsync(tag.Id);

            if (tagToAttach == null)
                throw new ArgumentException("Tag with this name doesn't exist!");

            postToChange.PostsSearchTags.Add(new PostsSearchTag { PhotoPostId = postToChange.Id, SearchTagId = tagToAttach.Id });
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DetachTagFromPost(SearchTagDTO tag, PostDTO post)
        {
            PhotoPost postToChange = await _dbcontext.Posts.GetByKeyAsync(post.Id);
            SearchTag tagToDetach = await _dbcontext.SearchTags.GetByKeyAsync(tag.Id);

            IEnumerable <PostsSearchTag> postToChangeSearchTag = postToChange.PostsSearchTags.Where(pst => pst.PhotoPostId == postToChange.Id && pst.SearchTagId == tagToDetach.Id);

            if (postToChangeSearchTag.FirstOrDefault() != null)
                postToChange.PostsSearchTags.Remove(postToChangeSearchTag.FirstOrDefault());

            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<SearchTagDTO>> GetPostTags(PostDTO post)
        {
            Queue<SearchTagDTO> resultTags = new Queue<SearchTagDTO>();
            PhotoPost seekedPost = await _dbcontext.Posts.GetByKeyAsync(post.Id);
            IEnumerable<PostsSearchTag> postSearchTags = seekedPost.PostsSearchTags;

            foreach (var tag in postSearchTags)
            {
                resultTags.Enqueue(new SearchTagDTO {
                    Id = tag.SearchTagNav.Id,
                    Name = tag.SearchTagNav.Name
                });
            }

            return resultTags;
        }

        public async Task RemoveTag(SearchTagDTO tag)
        {
            SearchTag tagToDelete = await _dbcontext.SearchTags.GetByKeyAsync(tag.Id);
            if (tagToDelete != null)
                _dbcontext.SearchTags.Delete(tagToDelete);

            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostDTO>> GetAllPostsByTag(SearchTagDTO tag)
        {
            Queue<PostDTO> resultPosts = new Queue<PostDTO>();
            IEnumerable <SearchTag> searchTag = await _dbcontext.SearchTags.GetByConditionAsync(t => t.Name == tag.Name);

            if (searchTag.FirstOrDefault() == null)
                return null;

            IEnumerable<PostsSearchTag> postsOfSearchTag = searchTag
                .FirstOrDefault()
                .PostsSearchTags;

            foreach (var post in postsOfSearchTag)
                resultPosts.Enqueue(new PostDTO
                {
                    Id = post.PhotoPostNav.Id,
                    Description = post.PhotoPostNav.Description,
                    PostingDate = post.PhotoPostNav.PostingDate
                });

            return resultPosts;
        }

        public async Task<IEnumerable<PostDTO>> GetPostsRangeByTag(SearchTagDTO tag, int from, int to)
        {
            Queue<PostDTO> resultPosts = new Queue<PostDTO>();
            IEnumerable<SearchTag> searchTag = await _dbcontext.SearchTags.GetByConditionAsync(t => t.Name == tag.Name);

            if (searchTag.FirstOrDefault() == null)
                return null;

            IEnumerable<PostsSearchTag> postsOfSearchTag = searchTag
                .FirstOrDefault()
                .PostsSearchTags
                .Skip(from)
                .Take(to);

            foreach (var post in postsOfSearchTag)
                resultPosts.Enqueue(new PostDTO
                {
                    Id = post.PhotoPostNav.Id,
                    Description = post.PhotoPostNav.Description,
                    PostingDate = post.PhotoPostNav.PostingDate
                });

            return resultPosts;
        }

        public async Task<SearchTagDTO> GetTagByName(string name)
        {
            IEnumerable<SearchTag> searchTags = await _dbcontext.SearchTags.GetByConditionAsync(s => s.Name == name);
            SearchTag returnTag = searchTags.FirstOrDefault();

            if (returnTag == null)
                return null;
            else
                return new SearchTagDTO {
                    Id = returnTag.Id,
                    Name = returnTag.Name
                };
        }

        public void Dispose() { _dbcontext.Dispose(); }
    }
}

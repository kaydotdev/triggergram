using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using PhotoAlbumDAL.Interfaces;
using PhotoAlbumDAL.Contexts;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Repositories
{
    /// <summary>
    /// Default repository for 'PHOTO_POST' entity.
    /// Supports sync and async repository methods.
    /// Type of record loading: explicit.
    /// </summary>
    public class PostRepository : IPostRepository
    {
        private ApplicationContext _dbcontext;
        public PostRepository(ApplicationContext context) { _dbcontext = context; }

        public void Create(PhotoPost entity) { _dbcontext.Posts.Add(entity); }

        public async Task CreateAsync(PhotoPost entity) { await _dbcontext.Posts.AddAsync(entity); }

        public void Delete(PhotoPost entity) { _dbcontext.Posts.Remove(entity); }

        public async Task DeleteAsync(PhotoPost entity) { await Task.Run(() => _dbcontext.Posts.Remove(entity)); }

        public void DeleteByKey(int key)
        {
            PhotoPost post = _dbcontext.Posts.Find(key);
            if (post != null) Delete(post);
        }

        public async Task DeleteByKeyAsync(int key)
        {
            PhotoPost post = await _dbcontext.Posts.FindAsync(key);
            if (post != null) Delete(post);
        }

        public IEnumerable<PhotoPost> GetAll()
        {
            IEnumerable<PhotoPost> posts = _dbcontext.Posts;

            foreach (var post in posts)
            {
                _dbcontext.Entry(post).Reference(p => p.UserNav).Load();
                _dbcontext.Entry(post).Reference(p => p.PhotoNav).Load();
                _dbcontext.Entry(post).Collection(p => p.PostsEmojiMarks).Load();
                _dbcontext.Entry(post).Collection(p => p.PostsSearchTags).Load();
                _dbcontext.Entry(post).Collection(p => p.PostsComments).Load();
            }

            return posts;
        }

        public async Task<IEnumerable<PhotoPost>> GetAllAsync()
        {
            IEnumerable<PhotoPost> posts = _dbcontext.Posts;

            foreach (var post in posts)
            {
                await _dbcontext.Entry(post).Reference(p => p.UserNav).LoadAsync();
                await _dbcontext.Entry(post).Reference(p => p.PhotoNav).LoadAsync();
                await _dbcontext.Entry(post).Collection(p => p.PostsEmojiMarks).LoadAsync();
                await _dbcontext.Entry(post).Collection(p => p.PostsSearchTags).LoadAsync();
                await _dbcontext.Entry(post).Collection(p => p.PostsComments).LoadAsync();
            }

            return posts;
        }

        public IEnumerable<PhotoPost> GetByCondition(Func<PhotoPost, bool> predicate)
        {
            IEnumerable<PhotoPost> posts = _dbcontext.Posts.Where(predicate);

            foreach (var post in posts)
            {
                _dbcontext.Entry(post).Reference(p => p.UserNav).Load();
                _dbcontext.Entry(post).Reference(p => p.PhotoNav).Load();
                _dbcontext.Entry(post).Collection(p => p.PostsEmojiMarks).Load();
                _dbcontext.Entry(post).Collection(p => p.PostsSearchTags).Load();
                _dbcontext.Entry(post).Collection(p => p.PostsComments).Load();
            }

            return posts;
        }

        public async Task<IEnumerable<PhotoPost>> GetByConditionAsync(Func<PhotoPost, bool> predicate)
        {
            IEnumerable<PhotoPost> posts = _dbcontext.Posts.Where(predicate);

            foreach (var post in posts)
            {
                await _dbcontext.Entry(post).Reference(p => p.UserNav).LoadAsync();
                await _dbcontext.Entry(post).Reference(p => p.PhotoNav).LoadAsync();
                await _dbcontext.Entry(post).Collection(p => p.PostsEmojiMarks).LoadAsync();
                await _dbcontext.Entry(post).Collection(p => p.PostsSearchTags).LoadAsync();
                await _dbcontext.Entry(post).Collection(p => p.PostsComments).LoadAsync();
            }

            return posts;
        }

        public PhotoPost GetByKey(int key)
        {
            PhotoPost post = _dbcontext.Posts.Find(key);

            if (post != null)
            {
                _dbcontext.Entry(post).Reference(p => p.UserNav).Load();
                _dbcontext.Entry(post).Reference(p => p.PhotoNav).Load();
                _dbcontext.Entry(post).Collection(p => p.PostsEmojiMarks).Load();
                _dbcontext.Entry(post).Collection(p => p.PostsSearchTags).Load();
                _dbcontext.Entry(post).Collection(p => p.PostsComments).Load();
            }

            return post;
        }

        public async Task<PhotoPost> GetByKeyAsync(int key)
        {
            PhotoPost post = _dbcontext.Posts.Find(key);

            if (post != null)
            {
                await _dbcontext.Entry(post).Reference(p => p.UserNav).LoadAsync();
                await _dbcontext.Entry(post).Reference(p => p.PhotoNav).LoadAsync();
                await _dbcontext.Entry(post).Collection(p => p.PostsEmojiMarks).LoadAsync();
                await _dbcontext.Entry(post).Collection(p => p.PostsSearchTags).LoadAsync();
                await _dbcontext.Entry(post).Collection(p => p.PostsComments).LoadAsync();
            }

            return post;
        }

        public void Update(PhotoPost entity) { _dbcontext.Posts.Update(entity); }

        public async Task UpdateAsync(PhotoPost entity) { await Task.Run(() => _dbcontext.Posts.Update(entity)); }
    }
}

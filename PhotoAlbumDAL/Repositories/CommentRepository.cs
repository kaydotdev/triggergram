using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using PhotoAlbumDAL.Interfaces;
using PhotoAlbumDAL.Contexts;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private ApplicationContext _dbcontext;
        public CommentRepository(ApplicationContext context) { _dbcontext = context; }

        public void Create(PhotoPostComment entity) { _dbcontext.Comments.Add(entity); }

        public async Task CreateAsync(PhotoPostComment entity) { await _dbcontext.Comments.AddAsync(entity); }

        public void Delete(PhotoPostComment entity) { _dbcontext.Comments.Remove(entity); }

        public async Task DeleteAsync(PhotoPostComment entity) { await Task.Run(() => _dbcontext.Comments.Remove(entity)); }

        public void DeleteByKey(int key)
        {
            PhotoPostComment comment = _dbcontext.Comments.Find(key);
            if (comment != null) Delete(comment);
        }

        public async Task DeleteByKeyAsync(int key)
        {
            PhotoPostComment comment = await _dbcontext.Comments.FindAsync(key);
            if (comment != null) Delete(comment);
        }

        public IEnumerable<PhotoPostComment> GetAll()
        {
            IEnumerable<PhotoPostComment> comments = _dbcontext.Comments;

            foreach (PhotoPostComment comment in comments)
                _dbcontext.Entry(comment).Reference(c => c.PhotoPostNav).Load();

            return comments;
        }

        public async Task<IEnumerable<PhotoPostComment>> GetAllAsync()
        {
            IEnumerable<PhotoPostComment> comments = _dbcontext.Comments;

            foreach (PhotoPostComment comment in comments)
                await _dbcontext.Entry(comment).Reference(c => c.PhotoPostNav).LoadAsync();

            return comments;
        }

        public IEnumerable<PhotoPostComment> GetByCondition(Func<PhotoPostComment, bool> predicate)
        {
            IEnumerable<PhotoPostComment> comments = _dbcontext.Comments.Where(predicate);

            foreach (PhotoPostComment comment in comments)
                _dbcontext.Entry(comment).Reference(c => c.PhotoPostNav).Load();

            return comments;
        }

        public async Task<IEnumerable<PhotoPostComment>> GetByConditionAsync(Func<PhotoPostComment, bool> predicate)
        {
            IEnumerable<PhotoPostComment> comments = _dbcontext.Comments.Where(predicate);

            foreach (PhotoPostComment comment in comments)
                await _dbcontext.Entry(comment).Reference(c => c.PhotoPostNav).LoadAsync();

            return comments;
        }

        public PhotoPostComment GetByKey(int key)
        {
            PhotoPostComment comment = _dbcontext.Comments.Find(key);

            if (comment != null)
                _dbcontext.Entry(comment).Reference(c => c.PhotoPostNav).Load();

            return comment;
        }

        public async Task<PhotoPostComment> GetByKeyAsync(int key)
        {
            PhotoPostComment comment = await _dbcontext.Comments.FindAsync(key);

            if (comment != null)
                await _dbcontext.Entry(comment).Reference(c => c.PhotoPostNav).LoadAsync();

            return comment;
        }

        public void Update(PhotoPostComment entity) { _dbcontext.Comments.Update(entity); }

        public async Task UpdateAsync(PhotoPostComment entity) { await Task.Run(() => _dbcontext.Comments.Update(entity)); }
    }
}

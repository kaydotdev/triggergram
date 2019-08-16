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
    /// Default repository for 'SEARCH_TAG' entity.
    /// Supports sync and async repository methods.
    /// Type of record loading: explicit.
    /// </summary>
    public class SearchTagRepository : ISearchTagRepository
    {
        private ApplicationContext _dbcontext;
        public SearchTagRepository(ApplicationContext context) { _dbcontext = context; }

        public void Create(SearchTag entity) { _dbcontext.Tags.Add(entity); }

        public async Task CreateAsync(SearchTag entity) { await _dbcontext.Tags.AddAsync(entity); }

        public void Delete(SearchTag entity) { _dbcontext.Tags.Remove(entity); }

        public async Task DeleteAsync(SearchTag entity) { await Task.Run(() => _dbcontext.Tags.Remove(entity)); }

        public void DeleteByKey(int key)
        {
            SearchTag tag = _dbcontext.Tags.Find(key);
            if (tag != null) Delete(tag);
        }

        public async Task DeleteByKeyAsync(int key)
        {
            SearchTag tag = await _dbcontext.Tags.FindAsync(key);
            if (tag != null) Delete(tag);
        }

        public IEnumerable<SearchTag> GetAll()
        {
            IEnumerable<SearchTag> tags = _dbcontext.Tags;

            foreach (var tag in tags)
                _dbcontext.Entry(tag).Collection(s => s.PostsSearchTags).Load();

            return tags;
        }

        public async Task<IEnumerable<SearchTag>> GetAllAsync()
        {
            IEnumerable<SearchTag> tags = _dbcontext.Tags;

            foreach (var tag in tags)
                await _dbcontext.Entry(tag).Collection(s => s.PostsSearchTags).LoadAsync();

            return tags;
        }

        public IEnumerable<SearchTag> GetByCondition(Func<SearchTag, bool> predicate)
        {
            IEnumerable<SearchTag> tags = _dbcontext.Tags.Where(predicate);

            foreach (var tag in tags)
                _dbcontext.Entry(tag).Collection(s => s.PostsSearchTags).Load();

            return tags;
        }

        public async Task<IEnumerable<SearchTag>> GetByConditionAsync(Func<SearchTag, bool> predicate)
        {
            IEnumerable<SearchTag> tags = _dbcontext.Tags.Where(predicate);

            foreach (var tag in tags)
                await _dbcontext.Entry(tag).Collection(s => s.PostsSearchTags).LoadAsync();

            return tags;
        }

        public SearchTag GetByKey(int key)
        {
            SearchTag tag = _dbcontext.Tags.Find(key);

            if (tag != null)
                _dbcontext.Entry(tag).Collection(s => s.PostsSearchTags).Load();

            return tag;
        }

        public async Task<SearchTag> GetByKeyAsync(int key)
        {
            SearchTag tag = _dbcontext.Tags.Find(key);

            if (tag != null)
                await _dbcontext.Entry(tag).Collection(s => s.PostsSearchTags).LoadAsync();

            return tag;
        }

        public void Update(SearchTag entity) { _dbcontext.Tags.Update(entity); }

        public async Task UpdateAsync(SearchTag entity) { await Task.Run(() => _dbcontext.Tags.Update(entity)); }
    }
}

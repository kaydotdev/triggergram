using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using PhotoAlbumDAL.Interfaces;
using PhotoAlbumDAL.Contexts;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Repositories
{
    public class EmojiRepository : IEmojiRepository
    {
        private ApplicationContext _dbcontext;
        public EmojiRepository(ApplicationContext context) { _dbcontext = context; }

        public void Create(EmojiMark entity) { _dbcontext.EmojiMarks.Add(entity); }

        public async Task CreateAsync(EmojiMark entity) { await _dbcontext.EmojiMarks.AddAsync(entity); }

        public void Delete(EmojiMark entity) { _dbcontext.EmojiMarks.Remove(entity); }

        public async Task DeleteAsync(EmojiMark entity) { await Task.Run(() => _dbcontext.EmojiMarks.Remove(entity)); }

        public void DeleteByKey(string key)
        {
            EmojiMark emoji = _dbcontext.EmojiMarks.Find(key);
            if (emoji != null) Delete(emoji);
        }

        public async Task DeleteByKeyAsync(string key)
        {
            EmojiMark emoji = await _dbcontext.EmojiMarks.FindAsync(key);
            if (emoji != null) Delete(emoji);
        }

        public IEnumerable<EmojiMark> GetAll()
        {
            IEnumerable<EmojiMark> emojis = _dbcontext.EmojiMarks;

            foreach (var emoji in emojis)
                _dbcontext.Entry(emoji).Collection(e => e.PostsEmojiMarks).Load();

            return emojis;
        }

        public async Task<IEnumerable<EmojiMark>> GetAllAsync()
        {
            IEnumerable<EmojiMark> emojis = _dbcontext.EmojiMarks;

            foreach (var emoji in emojis)
                await _dbcontext.Entry(emoji).Collection(e => e.PostsEmojiMarks).LoadAsync();

            return emojis;
        }

        public IEnumerable<EmojiMark> GetByCondition(Func<EmojiMark, bool> predicate)
        {
            IEnumerable<EmojiMark> emojis = _dbcontext.EmojiMarks.Where(predicate);

            foreach (var emoji in emojis)
                _dbcontext.Entry(emoji).Collection(e => e.PostsEmojiMarks).Load();

            return emojis;
        }

        public async Task<IEnumerable<EmojiMark>> GetByConditionAsync(Func<EmojiMark, bool> predicate)
        {
            IEnumerable<EmojiMark> emojis = _dbcontext.EmojiMarks.Where(predicate);

            foreach (var emoji in emojis)
                await _dbcontext.Entry(emoji).Collection(e => e.PostsEmojiMarks).LoadAsync();

            return emojis;
        }

        public EmojiMark GetByKey(string key)
        {
            EmojiMark emoji = _dbcontext.EmojiMarks.Find(key);

            if (emoji != null)
                _dbcontext.Entry(emoji).Collection(e => e.PostsEmojiMarks).Load();

            return emoji;
        }

        public async Task<EmojiMark> GetByKeyAsync(string key)
        {
            EmojiMark emoji = _dbcontext.EmojiMarks.Find(key);

            if (emoji != null)
                await _dbcontext.Entry(emoji).Collection(e => e.PostsEmojiMarks).LoadAsync();

            return emoji;
        }

        public void Update(EmojiMark entity) { _dbcontext.EmojiMarks.Update(entity); }

        public async Task UpdateAsync(EmojiMark entity) { await Task.Run(() => _dbcontext.EmojiMarks.Update(entity)); }
    }
}

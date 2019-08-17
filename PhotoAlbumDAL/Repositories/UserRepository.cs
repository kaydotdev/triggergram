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
    /// Default repository for 'USER' entity.
    /// Supports sync and async repository methods.
    /// Type of record loading: explicit.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private ApplicationContext _dbcontext;
        public UserRepository(ApplicationContext context) { _dbcontext = context; }

        public void Create(User entity) { _dbcontext.Users.Add(entity); }

        public async Task CreateAsync(User entity) { await _dbcontext.Users.AddAsync(entity); }

        public void Delete(User entity) { _dbcontext.Users.Remove(entity); }

        public async Task DeleteAsync(User entity) { await Task.Run(() => _dbcontext.Users.Remove(entity)); }

        public void DeleteByKey(int key)
        {
            User user = _dbcontext.Users.Find(key);
            if (user != null) Delete(user);
        }

        public async Task DeleteByKeyAsync(int key)
        {
            User user = await _dbcontext.Users.FindAsync(key);
            if (user != null) Delete(user);
        }

        public IEnumerable<User> GetAll()
        {
            IEnumerable<User> users = _dbcontext.Users;

            foreach (var user in users)
            {
                _dbcontext.Entry(user).Reference(u => u.UserRoleNav).Load();
                _dbcontext.Entry(user).Collection(u => u.PhotoPosts).Load();
                _dbcontext.Entry(user).Collection(u => u.PhotoPostComments).Load();
                _dbcontext.Entry(user).Collection(u => u.EmojiMarks).Load();
            }

            return users;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            IEnumerable<User> users = _dbcontext.Users;

            foreach (var user in users)
            {
                await _dbcontext.Entry(user).Reference(u => u.UserRoleNav).LoadAsync();
                await _dbcontext.Entry(user).Collection(u => u.PhotoPosts).LoadAsync();
                await _dbcontext.Entry(user).Collection(u => u.PhotoPostComments).LoadAsync();
                await _dbcontext.Entry(user).Collection(u => u.EmojiMarks).LoadAsync();
            }

            return users;
        }

        public IEnumerable<User> GetByCondition(Func<User, bool> predicate)
        {
            IEnumerable<User> users = _dbcontext.Users.Where(predicate);

            foreach (var user in users)
            {
                _dbcontext.Entry(user).Reference(u => u.UserRoleNav).Load();
                _dbcontext.Entry(user).Collection(u => u.PhotoPosts).Load();
                _dbcontext.Entry(user).Collection(u => u.PhotoPostComments).Load();
                _dbcontext.Entry(user).Collection(u => u.EmojiMarks).Load();
            }

            return users;
        }

        public async Task<IEnumerable<User>> GetByConditionAsync(Func<User, bool> predicate)
        {
            IEnumerable<User> users = _dbcontext.Users.Where(predicate);

            foreach (var user in users)
            {
                await _dbcontext.Entry(user).Reference(u => u.UserRoleNav).LoadAsync();
                await _dbcontext.Entry(user).Collection(u => u.PhotoPosts).LoadAsync();
                await _dbcontext.Entry(user).Collection(u => u.PhotoPostComments).LoadAsync();
                await _dbcontext.Entry(user).Collection(u => u.EmojiMarks).LoadAsync();
            }

            return users;
        }

        public User GetByKey(int key)
        {
            User user = _dbcontext.Users.Find(key);

            if (user != null)
            {
                _dbcontext.Entry(user).Reference(u => u.UserRoleNav).Load();
                _dbcontext.Entry(user).Collection(u => u.PhotoPosts).Load();
                _dbcontext.Entry(user).Collection(u => u.PhotoPostComments).Load();
                _dbcontext.Entry(user).Collection(u => u.EmojiMarks).Load();
            }

            return user;
        }

        public async Task<User> GetByKeyAsync(int key)
        {
            User user = _dbcontext.Users.Find(key);

            if (user != null)
            {
                await _dbcontext.Entry(user).Reference(u => u.UserRoleNav).LoadAsync();
                await _dbcontext.Entry(user).Collection(u => u.PhotoPosts).LoadAsync();
                await _dbcontext.Entry(user).Collection(u => u.PhotoPostComments).LoadAsync();
                await _dbcontext.Entry(user).Collection(u => u.EmojiMarks).LoadAsync();
            }

            return user;
        }

        public void Update(User entity) { _dbcontext.Users.Update(entity); }

        public async Task UpdateAsync(User entity) { await Task.Run(() => _dbcontext.Users.Update(entity)); }
    }
}

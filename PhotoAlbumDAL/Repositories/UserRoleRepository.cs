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
    /// Default repository for 'USER_ROLE' entity.
    /// Supports sync and async repository methods.
    /// Type of record loading: explicit.
    /// </summary>
    public class UserRoleRepository : IUserRoleRepository
    {
        private ApplicationContext _dbcontext;
        public UserRoleRepository(ApplicationContext context) { _dbcontext = context; }

        public void Create(UserRole entity) { _dbcontext.UserRoles.Add(entity); }

        public async Task CreateAsync(UserRole entity) { await _dbcontext.UserRoles.AddAsync(entity); }

        public void Delete(UserRole entity) { _dbcontext.UserRoles.Remove(entity); }

        public async Task DeleteAsync(UserRole entity) { await Task.Run(() => _dbcontext.UserRoles.Remove(entity)); }

        public void DeleteByKey(int key)
        {
            UserRole role = _dbcontext.UserRoles.Find(key);
            if (role != null) Delete(role);
        }

        public async Task DeleteByKeyAsync(int key)
        {
            UserRole role = await _dbcontext.UserRoles.FindAsync(key);
            if (role != null) Delete(role);
        }

        public IEnumerable<UserRole> GetAll()
        {
            IEnumerable<UserRole> roles = _dbcontext.UserRoles;

            foreach (var role in roles)
                _dbcontext.Entry(role).Collection(r => r.Users).Load();

            return roles;
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            IEnumerable<UserRole> roles = _dbcontext.UserRoles;

            foreach (var role in roles)
                await _dbcontext.Entry(role).Collection(r => r.Users).LoadAsync();

            return roles;
        }

        public IEnumerable<UserRole> GetByCondition(Func<UserRole, bool> predicate)
        {
            IEnumerable<UserRole> roles = _dbcontext.UserRoles.Where(predicate);

            foreach (var role in roles)
                _dbcontext.Entry(role).Collection(r => r.Users).Load();

            return roles;
        }

        public async Task<IEnumerable<UserRole>> GetByConditionAsync(Func<UserRole, bool> predicate)
        {
            IEnumerable<UserRole> roles = _dbcontext.UserRoles.Where(predicate);

            foreach (var role in roles)
                await _dbcontext.Entry(role).Collection(r => r.Users).LoadAsync();

            return roles;
        }

        public UserRole GetByKey(int key)
        {
            UserRole role = _dbcontext.UserRoles.Find(key);

            if (role != null)
                _dbcontext.Entry(role).Collection(r => r.Users).Load();

            return role;
        }

        public async Task<UserRole> GetByKeyAsync(int key)
        {
            UserRole role = _dbcontext.UserRoles.Find(key);

            if (role != null)
                await _dbcontext.Entry(role).Collection(r => r.Users).LoadAsync();

            return role;
        }

        public void Update(UserRole entity) { _dbcontext.UserRoles.Update(entity); }

        public async Task UpdateAsync(UserRole entity) { await Task.Run(() => _dbcontext.UserRoles.Update(entity)); }
    }
}

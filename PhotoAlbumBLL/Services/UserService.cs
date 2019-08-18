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
    public class UserService : IUserService
    {
        private IUnitOfWork _dbcontext;
        public UserService(IUnitOfWork context) { _dbcontext = context; }

        public async Task DeleteUser(UserDTO user)
        {
            IEnumerable<User> users = await _dbcontext.Users.GetByConditionAsync(u => u.Nickname == user.UserName);
            User userToDelete = users.FirstOrDefault();

            if (userToDelete != null)
                _dbcontext.Users.Delete(userToDelete);

            await _dbcontext.SaveChangesAsync();
        }

        public async Task PromoteUser(UserDTO user, UserRoleDTO role)
        {
            IEnumerable<User> users = await _dbcontext.Users.GetByConditionAsync(u => u.Nickname == user.UserName);
            User userToPromote = users.FirstOrDefault();
            UserRole roleFroUser = await _dbcontext.UserRoles.GetByKeyAsync(role.Id);

            if (roleFroUser == null)
                throw new ArgumentException("This role doesn't exist!");

            if (userToPromote == null)
                throw new ArgumentException("Can't promote user that doesn't exist!");

            userToPromote.RoleId = roleFroUser.Id;
            await _dbcontext.SaveChangesAsync();
        }

        public void Dispose() { _dbcontext.Dispose(); }

        public async Task<UserDTO> GetUserByUserName(string username)
        {
            IEnumerable<User> users = await _dbcontext.Users.GetByConditionAsync(u => u.Nickname == username);
            User userToPromote = users.FirstOrDefault();

            return new UserDTO { UserName = userToPromote.Nickname };
        }
    }
}

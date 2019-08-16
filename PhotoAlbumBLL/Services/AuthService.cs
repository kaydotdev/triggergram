using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;

using PhotoAlbumDAL.Models;
using PhotoAlbumDAL.Interfaces;

using PhotoAlbumBLL.DTO;
using PhotoAlbumBLL.Interfaces;
using PhotoAlbumBLL.Security;

namespace PhotoAlbumBLL.Services
{
    public class AuthService : IAuthService
    {
        private IUnitOfWork _dbcontext;

        public AuthService(IUnitOfWork context) { _dbcontext = context; }

        public async Task<ClaimedUserDTO> Login(UserDTO user)
        {
            IEnumerable<User> seekedUsers = await _dbcontext.Users.GetByConditionAsync(u => u.Nickname == user.UserName);
            User seekedUser = seekedUsers.FirstOrDefault();

            if (seekedUser == null) return null;

            if (!VerifyPassword(user.Password, new EncryptedPassword
            {
                PasswordHash = seekedUser.PasswordHash,
                PasswordSalt = seekedUser.PasswordSalt
            }))
                return null;

            return new ClaimedUserDTO {
                UserId = seekedUser.Id,
                UserName = seekedUser.Nickname,
                Password = user.Password,
                RoleId = seekedUser.RoleId
            };
        }

        private bool VerifyPassword(string Password, EncryptedPassword encr)
        {
            using (HMACSHA512 alg = new HMACSHA512(encr.PasswordSalt))
            {
                byte[] computedHash = alg.ComputeHash(Encoding.UTF8.GetBytes(Password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != encr.PasswordHash[i])
                        return false;
                }
            }

            return true;
        }

        public async Task SignUp(NewUserDTO user)
        {
            if (user.Password != user.PasswordConfirmation)
                throw new ArgumentException("Password and confirmation should match!");

            EncryptedPassword encr = CreatePasswordHashAndSalt(user.Password);

            await _dbcontext.Users.CreateAsync(new User {
                Nickname = user.Username,
                PasswordHash = encr.PasswordHash,
                PasswordSalt = encr.PasswordSalt,
                RoleId = _dbcontext.UserRoles.GetByKey(1).Id,
            });

            await _dbcontext.SaveChangesAsync();
        }

        private EncryptedPassword CreatePasswordHashAndSalt(string password)
        {
            EncryptedPassword result = new EncryptedPassword();

            using (HMACSHA512 alg = new HMACSHA512())
            {
                result.PasswordSalt = alg.Key;
                result.PasswordHash = alg.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            return result;
        }

        public void Dispose() { _dbcontext.Dispose(); }

        public async Task<bool> UserExists(NewUserDTO user)
        {
            IEnumerable<User> seekedUsers = await _dbcontext.Users.GetByConditionAsync(u => u.Nickname == user.Username);
            User seekedUser = seekedUsers.FirstOrDefault();

            return seekedUser != null;
        }
    }
}

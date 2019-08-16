using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Configs
{
    /// <summary>
    /// Essential configuration for entity 'USER_ROLE'.
    /// Adds default roles for users!
    /// </summary>
    public class UserRoleDefaultDataSet : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(new UserRole[] {
                new UserRole { Id = 1, Name = "User" },
                new UserRole { Id = 2, Name = "Moderator" },
                new UserRole { Id = 3, Name = "Admin" }
            });
        }
    }
}

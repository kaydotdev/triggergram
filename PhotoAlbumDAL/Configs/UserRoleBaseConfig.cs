using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Configs
{
    /// <summary>
    /// Essential configuration for entity 'USER_ROLE'.
    /// Sets all keys, names, types.
    /// </summary>
    public class UserRoleBaseConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("USER_ROLE")
                .HasKey(ur => ur.Id)
                .HasName("UserRolePK");

            builder.Property(ur => ur.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("user_role_id");

            builder.Property(ur => ur.Name)
                .IsRequired()
                .HasColumnName("user_role_name")
                .HasColumnType("varchar(250)");
        }
    }
}

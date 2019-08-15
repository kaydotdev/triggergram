using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Configs
{
    /// <summary>
    /// Essential configuration for entity 'USER'.
    /// Sets all keys, names, types and references.
    /// </summary>
    public class UserBaseConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USER")
                .HasKey(u => u.Id)
                .HasName("UserPK");

            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("user_id");

            builder.Property(u => u.Nickname)
                .IsRequired()
                .HasColumnName("user_nickname")
                .HasColumnType("varchar(250)");

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasColumnName("password_hash")
                .HasColumnType("varbinary(max)");

            builder.Property(u => u.PasswordSalt)
                .IsRequired()
                .HasColumnName("password_salt")
                .HasColumnType("varbinary(max)");

            builder.Property(u => u.RoleId)
                .HasColumnName("role_id");

            builder.HasOne(u => u.UserRoleNav)
                .WithMany(ur => ur.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

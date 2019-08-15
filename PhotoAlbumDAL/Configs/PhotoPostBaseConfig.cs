using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Configs
{
    /// <summary>
    /// Essential configuration for entity 'PHOTO_POST'.
    /// Sets all keys, names, types and references.
    /// </summary>
    public class PhotoPostBaseConfig : IEntityTypeConfiguration<PhotoPost>
    {
        public void Configure(EntityTypeBuilder<PhotoPost> builder)
        {
            builder.ToTable("PHOTO_POST")
                .HasKey(pp => pp.Id)
                .HasName("PhotoPostPK");

            builder.Property(pp => pp.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("post_id");

            builder.Property(pp => pp.Description)
                .HasColumnName("post_description")
                .HasColumnType("varchar(1000)");

            builder.Property(pp => pp.PostingDate)
                .IsRequired()
                .HasColumnName("posting_date")
                .HasColumnType("date");

            builder.Property(pp => pp.UserId)
                .HasColumnName("user_id");

            builder.Property(pp => pp.PhotoId)
                .HasColumnName("photo_id");

            builder.HasOne(pp => pp.PhotoNav)
                .WithOne(p => p.PhotoPostNav)
                .HasForeignKey<PhotoPost>(pp => pp.PhotoId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(pp => pp.UserNav)
                .WithMany(u => u.PhotoPosts)
                .HasForeignKey(pp => pp.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

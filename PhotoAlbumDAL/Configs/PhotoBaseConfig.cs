using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Configs
{
    /// <summary>
    /// Essential configuration for entity 'PHOTO'.
    /// Sets all keys, names, types and references.
    /// </summary>
    public class PhotoBaseConfig : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable("PHOTO")
                .HasKey(p => p.Id)
                .HasName("PhotoPK");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("photo_id");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("photo_name")
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Source)
                .IsRequired()
                .HasColumnName("photo_base64_source")
                .HasColumnType("image");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Configs
{
    /// <summary>
    /// Essential configuration for entity 'EMOJI_MARK'.
    /// Sets all keys, names and types.
    /// </summary>
    public class EmojiMarkBaseConfig : IEntityTypeConfiguration<EmojiMark>
    {
        public void Configure(EntityTypeBuilder<EmojiMark> builder)
        {
            builder.ToTable("EMOJI_MARK")
                .HasKey(em => em.Name)
                .HasName("EmojiMarkPK");

            builder.Property(em => em.Name)
                .ValueGeneratedNever()
                .HasColumnName("emoji_name")
                .HasColumnType("varchar(250)");

            builder.Property(em => em.Source)
                .IsRequired()
                .HasColumnName("emoji_base64_source")
                .HasColumnType("image");
        }
    }
}

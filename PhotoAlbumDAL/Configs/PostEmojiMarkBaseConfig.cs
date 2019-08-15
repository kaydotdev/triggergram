using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Configs
{
    /// <summary>
    /// Essential configuration for linking-entity between 'PHOTO_POST' and 'EMOJI_MARK'.
    /// Sets all keys, names, types and references.
    /// </summary>
    public class PostEmojiMarkBaseConfig : IEntityTypeConfiguration<PostsEmojiMark>
    {
        public void Configure(EntityTypeBuilder<PostsEmojiMark> builder)
        {
            builder.ToTable("PHOTO_POST_TO_EMOJI_MARK")
                .HasKey(ppem => new { ppem.PhotoPostId, ppem.EmojiName })
                .HasName("PhotoPostToEmojiMarkPK");

            builder.Property(ppem => ppem.PhotoPostId)
                .HasColumnName("post_id");

            builder.Property(ppem => ppem.EmojiName)
                .HasColumnName("emoji_name")
                .HasColumnType("varchar(250)");

            builder.HasOne(ppem => ppem.EmojiMarkNav)
                .WithMany(em => em.PostsEmojiMarks)
                .HasForeignKey(ppem => ppem.EmojiName);

            builder.HasOne(ppem => ppem.PhotoPostNav)
                .WithMany(pp => pp.PostsEmojiMarks)
                .HasForeignKey(ppem => ppem.PhotoPostId);
        }
    }
}

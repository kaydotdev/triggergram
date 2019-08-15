using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Configs
{
    /// <summary>
    /// Essential configuration for linking-entity between 'PHOTO_POST' and 'SEARCH_TAG'.
    /// Sets all keys, names, types and references.
    /// </summary>
    public class PostSearchTagBaseConfig : IEntityTypeConfiguration<PostsSearchTag>
    {
        public void Configure(EntityTypeBuilder<PostsSearchTag> builder)
        {
            builder.ToTable("PHOTO_POST_TO_SEARCH_TAG")
                .HasKey(ppst => new { ppst.PhotoPostId, ppst.SearchTagId })
                .HasName("PhotoPostToSearchTagPK");

            builder.Property(ppst => ppst.PhotoPostId)
                .HasColumnName("post_id");

            builder.Property(ppst => ppst.SearchTagId)
                .HasColumnName("search_tag_id");

            builder.HasOne(ppst => ppst.PhotoPostNav)
                .WithMany(pp => pp.PostsSearchTags)
                .HasForeignKey(ppst => ppst.PhotoPostId);

            builder.HasOne(ppst => ppst.SearchTagNav)
                .WithMany(st => st.PostsSearchTags)
                .HasForeignKey(ppst => ppst.SearchTagId);
        }
    }
}

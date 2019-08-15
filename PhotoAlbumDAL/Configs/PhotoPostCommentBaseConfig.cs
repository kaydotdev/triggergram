using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Configs
{
    /// <summary>
    /// Essential configuration for entity 'POST_COMMENT'.
    /// Sets all keys, names, types and references.
    /// </summary>
    public class PhotoPostCommentBaseConfig : IEntityTypeConfiguration<PhotoPostComment>
    {
        public void Configure(EntityTypeBuilder<PhotoPostComment> builder)
        {
            builder.ToTable("POST_COMMENT")
                .HasKey(ppc => ppc.Id)
                .HasName("PhotoPostCommentPK");

            builder.Property(ppc => ppc.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("comment_id");

            builder.Property(ppc => ppc.Content)
                .IsRequired()
                .HasColumnName("comment_content")
                .HasColumnType("varchar(1000)");

            builder.Property(ppc => ppc.PhotoPostId)
                .HasColumnName("post_id");

            builder.HasOne(ppc => ppc.PhotoPostNav)
                .WithMany(pp => pp.PostsComments)
                .HasForeignKey(ppc => ppc.PhotoPostId);
        }
    }
}

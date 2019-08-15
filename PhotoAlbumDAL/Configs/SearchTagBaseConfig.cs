using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Configs
{
    /// <summary>
    /// Essential configuration for entity 'SEARCH_TAG'.
    /// Sets all keys, names and types.
    /// </summary>
    public class SearchTagBaseConfig : IEntityTypeConfiguration<SearchTag>
    {
        public void Configure(EntityTypeBuilder<SearchTag> builder)
        {
            builder.ToTable("SEARCH_TAG")
                .HasKey(st => st.Id)
                .HasName("SearchTagPK");

            builder.Property(st => st.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("search_tag_id");

            builder.Property(st => st.Name)
                .HasColumnName("search_tag_name")
                .HasColumnType("varchar(250)");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MediaPostEntity = Triggergram.Core.Data.Models.MediaPost;

namespace Triggergram.Core.Data.Configs
{
    public class MediaPostMap : IEntityTypeConfiguration<MediaPostEntity>
    {
        public void Configure(EntityTypeBuilder<MediaPostEntity> builder)
        {
            builder.Property(mp => mp.Id)
                .HasMaxLength(50)
                .HasConversion(
                    id => id.ToString(),
                    id => Guid.Parse(id));

            builder.Property(mp => mp.Title)
                .IsRequired().HasMaxLength(250);

            builder.Property(mp => mp.Description)
                .IsRequired().HasMaxLength(2000);

            builder.Property(mp => mp.AccountId)
                .IsRequired().HasMaxLength(50)
                .HasConversion(
                    id => id.ToString(),
                    id => Guid.Parse(id));
        }
    }
}

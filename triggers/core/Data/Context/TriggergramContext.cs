using Microsoft.EntityFrameworkCore;
using MediaPostEntity = Triggergram.Core.Data.Models.MediaPost;
using Triggergram.Core.Data.Configs;

namespace Triggergram.Core.Data.Context
{
    public class TriggergramContext : DbContext
    {
        public DbSet<MediaPostEntity> MediaPosts { get; set; }

        public TriggergramContext(DbContextOptions<TriggergramContext> options)
            : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MediaPostMap());
        }
    }
}

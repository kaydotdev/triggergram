using Microsoft.EntityFrameworkCore;
using MediaPostEntity = Triggergram.Core.Data.Models.MediaPost;

namespace Triggergram.Core.Data.Context
{
    public class TriggergramContext : DbContext
    {
        public DbSet<MediaPostEntity> MediaPosts { get; set; }

        public TriggergramContext(DbContextOptions<TriggergramContext> options)
            : base(options) {}
    }
}

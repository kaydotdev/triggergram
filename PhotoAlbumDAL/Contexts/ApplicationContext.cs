using Microsoft.EntityFrameworkCore;

using PhotoAlbumDAL.Configs;
using PhotoAlbumDAL.Models;

namespace PhotoAlbumDAL.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<EmojiMark> EmojiMarks { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PhotoPost> Posts { get; set; }
        public DbSet<PhotoPostComment> Comments { get; set; }
        public DbSet<SearchTag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Base configs
            modelBuilder.ApplyConfiguration(new EmojiMarkBaseConfig());
            modelBuilder.ApplyConfiguration(new PhotoBaseConfig());
            modelBuilder.ApplyConfiguration(new PhotoPostBaseConfig());
            modelBuilder.ApplyConfiguration(new PhotoPostCommentBaseConfig());
            modelBuilder.ApplyConfiguration(new PostEmojiMarkBaseConfig());
            modelBuilder.ApplyConfiguration(new PostSearchTagBaseConfig());
            modelBuilder.ApplyConfiguration(new SearchTagBaseConfig());
            modelBuilder.ApplyConfiguration(new UserBaseConfig());
            modelBuilder.ApplyConfiguration(new UserRoleBaseConfig());

            // Default data configs
            modelBuilder.ApplyConfiguration(new EmojiDefaultDataSet());
            modelBuilder.ApplyConfiguration(new UserRoleDefaultDataSet());
        }
    }
}

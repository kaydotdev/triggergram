using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PhotoAlbumDAL.Contexts;

namespace PhotoAlbumDAL
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            DbContextOptions<ApplicationContext> options = optionsBuilder.UseSqlServer(@"Server=DESKTOP-7VVBMQ6\SQLEXPRESS;Database=WebPhotoAlbum;Trusted_Connection=True;").Options;

            return new ApplicationContext(options);
        }
    }
}

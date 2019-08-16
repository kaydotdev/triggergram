using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using PhotoAlbumDAL.Interfaces;
using PhotoAlbumDAL.Repositories;

namespace PhotoAlbumBLL.Dependencies
{
    public static class UnitOfWorkServiceProvider
    {
        public static void AddSQLServerUnitOfWorkByFileConfig(this IServiceCollection services, string filename, string connectionname)
        {
            services.AddScoped<IUnitOfWork>(uow => new UnitOfWork(filename, connectionname));
        }

        public static void AddSQLServerUnitOfWorkByConnectionString(this IServiceCollection services, string connectionstring)
        {
            services.AddScoped<IUnitOfWork>(s => new UnitOfWork(connectionstring));
        }

        public static void AddPostgreSQLUnitOfWorkByConnectionString(this IServiceCollection services, string connectionstring)
        {
            services.AddScoped<IUnitOfWork>(uow => new UnitOfWork(optionsSetup => optionsSetup.UseNpgsql(connectionstring)));
        }
    }
}

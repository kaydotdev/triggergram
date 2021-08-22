using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Triggergram.Core.Data.Context;

namespace Triggergram.Core.Data.Extensions
{
    public static class DataExtensions
    {
        public static void AddDataStorage(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TriggergramContext>(
                options => options.UseSqlServer(connectionString));
        }
    }
}

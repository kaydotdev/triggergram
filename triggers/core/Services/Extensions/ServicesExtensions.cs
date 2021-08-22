using Microsoft.Extensions.DependencyInjection;
using Triggergram.Core.Services.Contracts;
using Triggergram.Core.Services.Implementation;

namespace Triggergram.Core.Services.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddMediaConverters(this IServiceCollection services)
        {
            services.AddScoped<IMediaConverter, MediaConverter>();
        }

        public static void AddMediaContainers(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IMediaContainer>(provider => new MediaContainer(connectionString));
        }

        public static void AddMediaServices(this IServiceCollection services)
        {
            services.AddScoped<IMediaPostService, MediaPostService>();
        }
    }
}

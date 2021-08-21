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
    }
}

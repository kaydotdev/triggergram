using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Triggergram.Core.Services.Extensions;

[assembly: FunctionsStartup(typeof(Triggergram.Core.Startup))]
namespace Triggergram.Core
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddMediaConverters();
        }
    }
}

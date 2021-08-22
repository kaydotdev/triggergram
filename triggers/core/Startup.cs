using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Triggergram.Core.Data.Extensions;
using Triggergram.Core.Services.Extensions;

[assembly: FunctionsStartup(typeof(Triggergram.Core.Startup))]
namespace Triggergram.Core
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDataStorage(
                Environment.GetEnvironmentVariable("DATA_STORAGE_CONNECTION_STRING"));
            builder.Services.AddMediaConverters();
            builder.Services.AddMediaContainers(
                Environment.GetEnvironmentVariable("STORAGE_CONNECTION_STRING"));
            builder.Services.AddMediaServices();
        }
    }
}

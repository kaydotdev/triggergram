using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Triggergram.Core.Data.Context
{
    public class TriggergramContextFactory : IDesignTimeDbContextFactory<TriggergramContext>
    {
        public TriggergramContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TriggergramContext>();
            optionsBuilder.UseSqlServer(
                Environment.GetEnvironmentVariable("DATA_STORAGE_CONNECTION_STRING") ?? "");

            return new TriggergramContext(optionsBuilder.Options);
        }
    }
}

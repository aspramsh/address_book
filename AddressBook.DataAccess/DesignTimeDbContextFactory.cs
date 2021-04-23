using AddressBook.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AddressBook.DataAccess
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AddressBookContext>
    {
        public AddressBookContext CreateDbContext(string[] args)
        {
            var environment = EnvironmentHelper.GetCurrentEnvironmentVariable();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: false)
                .Build();

            var builder = new DbContextOptionsBuilder<AddressBookContext>();
            var connectionString = configuration.GetConnectionString(Constants.ConnectionName);
            builder.UseNpgsql(connectionString);

            return new AddressBookContext(builder.Options);
        }
    }
}

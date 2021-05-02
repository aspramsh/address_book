using AddressBook.DataAccess;
using AddressBook.DataAccess.DataSeeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.UnitTests
{
    public class FakeDataSeed : DataSeed
    {
        public FakeDataSeed(AddressBookContext context)
        {
            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().Location);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);

            var dataSeedPath = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json").Build()[Constants.DataSeedPath];

            DataSeedJsonFilesRootPath = Path.Combine(
                dirPath,
                Path.Combine(Directory.GetCurrentDirectory(), dataSeedPath));

            AddressBookContext = context;
        }

        public override async Task SeedAllInitialDataAsync(CancellationToken cancellationToken = default)
        {
            if (!AddressBookContext.Database.IsInMemory() || AddressBookContext.Country.Any())
            {
                return;
            }

            await SeedDatabaseAsync(cancellationToken);
        }
    }
}

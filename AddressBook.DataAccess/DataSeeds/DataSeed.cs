using AddressBook.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBook.DataAccess.DataSeeds
{
    public class DataSeed : IDataSeed, IDisposable
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IServiceScope _serviceScope;

        public DataSeed()
        {
        }

        public DataSeed(
            IServiceProvider serviceProvider,
            IOptions<DataSeedOptions> dataSeedOptions,
            IHostEnvironment hostEnvironment)
        {
            _serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope();
            AddressBookContext = _serviceScope.ServiceProvider.GetRequiredService<AddressBookContext>();
            DataSeedJsonFilesRootPath = dataSeedOptions.Value.JsonFilesRootPath;
            _hostEnvironment = hostEnvironment;
        }

        protected string DataSeedJsonFilesRootPath { get; set; }

        protected AddressBookContext AddressBookContext { get; set; }

        public virtual async Task SeedAllInitialDataAsync(CancellationToken cancellationToken = default)
        {
            if (_hostEnvironment.IsProduction() || AddressBookContext.Database.IsInMemory())
            {
                return;
            }

            var pendingMigrations =
                (await AddressBookContext.Database.GetPendingMigrationsAsync(cancellationToken)).ToList();

            if (!pendingMigrations.Any())
            {
                return;
            }

            var allMigrationsCount = AddressBookContext.Database.GetMigrations()?.Count();

            if (allMigrationsCount == pendingMigrations.Count)
            {
                await AddressBookContext.Database.EnsureDeletedAsync(cancellationToken);
            }

            await AddressBookContext.Database.MigrateAsync(cancellationToken);

            await SeedDatabaseAsync(cancellationToken: cancellationToken);
        }

        #region Read seed data from Json files

        public ICollection<Country> GetCountries()
        {
            return ReadSeedDataFromJsonFile<Country>(DataSeedJsonFilesRootPath, "Countries");
        }

        #endregion Read seed data from Json files

        public void Dispose()
        {
            _serviceScope.Dispose();
            AddressBookContext.Dispose();
        }

        protected async Task SeedDatabaseAsync(CancellationToken cancellationToken = default)
        {
            if (!await AddressBookContext.Country.AsNoTracking().AnyAsync(cancellationToken))
            {
                await AddressBookContext.Country.AddRangeAsync(GetCountries(), cancellationToken);
                await AddressBookContext.SaveChangesAsync(cancellationToken);
            }
        }

        #region private methods

        private static ICollection<T> ReadSeedDataFromJsonFile<T>(string path, string fileName)
        {
            var fullPath = Path.Combine(path, $"{fileName}.json");
            var data = File.ReadAllText(fullPath, Encoding.UTF8);

            return JsonConvert.DeserializeObject<ICollection<T>>(data);
        }

        #endregion private methods
    }
}

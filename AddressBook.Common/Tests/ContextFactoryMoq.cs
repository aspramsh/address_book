using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AddressBook.Common.Tests
{
    public abstract class ContextFactoryMoq<T>
        where T : DbContext
    {
        public T Context { get; set; }

        public void Create()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseInternalServiceProvider(serviceProvider)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            Context = (T)Activator.CreateInstance(typeof(T), builder.Options);

            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            SeedData();
            Context.SaveChanges();
        }

        public virtual void Destroy()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }

        protected abstract void SeedData();
    }
}

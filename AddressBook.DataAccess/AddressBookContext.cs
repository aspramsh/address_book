using AddressBook.Common.Extensions;
using AddressBook.DataAccess.Configurations;
using AddressBook.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.DataAccess
{
    public class AddressBookContext : DbContext
    {
        public AddressBookContext(DbContextOptions<AddressBookContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Address { get; set; }

        public DbSet<City> City { get; set; }

        public DbSet<Contact> Contact { get; set; }

        public DbSet<Country> Country { get; set; }

        public DbSet<PhoneNumber> PhoneNumber { get; set; }

        public DbSet<State> State { get; set; }

        public DbSet<ZipCode> ZipCode { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AddressConfiguration());

            builder.ApplyConfiguration(new CityConfiguration());

            builder.ApplyConfiguration(new ContactConfiguration());

            builder.ApplyConfiguration(new CountryConfiguration());

            builder.ApplyConfiguration(new PhoneNumberConfiguration());

            builder.ApplyConfiguration(new StateConfiguration());

            builder.ApplyConfiguration(new ZipCodeConfiguration());

            base.OnModelCreating(builder);

            builder.PostgresModelCreating();
        }
    }
}

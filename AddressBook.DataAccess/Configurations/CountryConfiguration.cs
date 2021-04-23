using AddressBook.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AddressBook.DataAccess.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(m => m.Name).IsRequired();
            builder.Property(m => m.Code).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasIndex(x => x.Code).IsUnique();
        }
    }
}
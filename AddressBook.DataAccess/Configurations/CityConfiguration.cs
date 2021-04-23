using AddressBook.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AddressBook.DataAccess.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(m => m.Name).IsRequired();
            builder.HasIndex(x => new { x.StateId, x.Name }).IsUnique();

            builder.HasOne(t => t.State)
                .WithMany(m => m.Cities)
                .HasForeignKey(bc => bc.StateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Country)
                .WithMany(m => m.Cities)
                .HasForeignKey(bc => bc.CountryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

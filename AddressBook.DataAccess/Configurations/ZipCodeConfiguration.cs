using AddressBook.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AddressBook.DataAccess.Configurations
{
    public class ZipCodeConfiguration : IEntityTypeConfiguration<ZipCode>
    {
        public void Configure(EntityTypeBuilder<ZipCode> builder)
        {
            builder.Property(m => m.Code).IsRequired();
            builder.HasIndex(x => new { x.CountryId, x.StateId, x.CityId, x.Code }).IsUnique();

            builder.HasOne(t => t.Country)
                .WithMany(m => m.ZipCodes)
                .HasForeignKey(bc => bc.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.State)
                .WithMany(m => m.ZipCodes)
                .HasForeignKey(bc => bc.StateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.City)
                .WithMany(m => m.ZipCodes)
                .HasForeignKey(bc => bc.CityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

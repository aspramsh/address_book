using AddressBook.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AddressBook.DataAccess.Configurations
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.Property(m => m.Name).IsRequired();
            builder.HasIndex(x => new { x.Name }).IsUnique();

            builder.HasOne(t => t.Country)
                .WithMany(m => m.States)
                .HasForeignKey(bc => bc.CountryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

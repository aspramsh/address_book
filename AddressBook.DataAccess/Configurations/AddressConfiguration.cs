using AddressBook.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AddressBook.DataAccess.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("NOW()");

            builder.Property(m => m.ContactId).IsRequired();

            builder.HasIndex(x => new { x.ContactId, x.ZipCodeId }).IsUnique();
        }
    }
}

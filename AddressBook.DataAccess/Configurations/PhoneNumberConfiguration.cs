using AddressBook.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AddressBook.DataAccess.Configurations
{
    public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            builder.Property(m => m.Phone).IsRequired();
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("NOW()");

            builder.HasIndex(x => new { x.Phone, x.PhoneType, x.ContactId }).IsUnique();

            builder.HasOne(t => t.Contact)
                .WithMany(m => m.PhoneNumbers)
                .HasForeignKey(bc => bc.ContactId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

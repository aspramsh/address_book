using AddressBook.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AddressBook.DataAccess.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("NOW()");

            builder.Property(m => m.FirstName).IsRequired();

            builder.Property(m => m.LastName).IsRequired();

            builder.Property(m => m.Email).IsRequired();

            builder.HasIndex(x => new { x.Email }).IsUnique();
        }
    }
}

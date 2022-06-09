using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Data.Entities;

namespace TestTask.Data.EntityConfigurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts");

            builder.HasKey(x => x.Id);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(c => c.Email).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasOne(c => c.Account)
                .WithMany(a => a.Contacts)
                .HasForeignKey(c => c.AccountId)
                .HasPrincipalKey(a => a.Id)
                .IsRequired();
        }
    }
}

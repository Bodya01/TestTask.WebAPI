using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Data.Entities;

namespace TestTask.Data.EntityConfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasOne(a => a.Incident)
                .WithMany(i => i.Accounts)
                .HasForeignKey(a => a.IncidentName)
                .HasPrincipalKey(i => i.IncidentName)
                .IsRequired();
        }
    }
}

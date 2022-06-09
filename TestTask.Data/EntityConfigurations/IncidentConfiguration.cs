using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Data.Entities;

namespace TestTask.Data.EntityConfigurations
{
    internal class IncidentConfiguration : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> builder)
        {
            builder.ToTable("Incidents");

            builder.HasKey(i => i.IncidentName);

            builder.Property(i => i.Description)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}

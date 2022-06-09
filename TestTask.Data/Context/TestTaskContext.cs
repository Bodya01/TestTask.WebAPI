using Microsoft.EntityFrameworkCore;
using TestTask.Data.Entities;
using TestTask.Data.EntityConfigurations;

namespace TestTask.Data.Context
{
    public class TestTaskContext : DbContext
    {
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public TestTaskContext(DbContextOptions<TestTaskContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new IncidentConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new IncidentConfiguration());
        }
    }
}

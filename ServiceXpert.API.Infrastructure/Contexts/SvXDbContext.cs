using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ServiceXpert.API.Domain.Entities;
using ServiceXpert.API.Domain.Shared.Enums.Database;
using System.Reflection;

namespace ServiceXpert.API.Infrastructure.Contexts
{
    public class SvXDbContext : DbContext
    {
        private string ConnectionString
        {
            get
            {
                string? connectionString = Environment.GetEnvironmentVariable("", EnvironmentVariableTarget.User);
                return connectionString != null ? connectionString : throw new KeyNotFoundException("Fatal: Missing connection string");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.ConnectionString);
            // optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType(nameof(DatabaseDataType.VARCHAR));
            configurationBuilder.Conventions.Remove(typeof(ForeignKeyIndexConvention));
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = this.ChangeTracker.Entries<EntityBase>();
            var utcNow = DateTime.SpecifyKind(
                new DateTime(DateTime.UtcNow.Ticks / TimeSpan.TicksPerSecond * TimeSpan.TicksPerSecond),
                DateTimeKind.Utc);

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateDate = utcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifyDate = utcNow;
                        break;
                }
            }
        }
    }
}

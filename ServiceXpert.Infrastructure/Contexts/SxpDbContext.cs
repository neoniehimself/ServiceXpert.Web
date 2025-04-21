using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared.Enums;
using System.Reflection;

namespace ServiceXpert.Infrastructure.Contexts
{
    public class SxpDbContext : DbContext
    {
        private string ConnectionString
        {
            get
            {
                string? connectionString = Environment.GetEnvironmentVariable("ServiceXpert", EnvironmentVariableTarget.User);
                return connectionString != null ? connectionString : throw new KeyNotFoundException("Fatal: Missing connection string");
            }
        }

        public DbSet<Issue> Issue { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.ConnectionString,
                sqlServerOptionsAction => sqlServerOptionsAction.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

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

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var utcNow = DateTime.UtcNow;
            foreach (var entry in this.ChangeTracker.Entries<EntityBase>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateDate = utcNow;
                }
                entry.Entity.ModifyDate = utcNow;
            }
        }
    }
}

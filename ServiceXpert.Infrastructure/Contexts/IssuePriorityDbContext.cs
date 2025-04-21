using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities;
using DomainEnums = ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Infrastructure.Contexts
{
    internal class IssuePriorityDbContext : DbContextBase, IEntityTypeConfiguration<IssuePriority>
    {
        private readonly DateTime dateTime = new(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc);

        public void Configure(EntityTypeBuilder<IssuePriority> issuePriority)
        {
            issuePriority.HasKey(i => i.IssuePriorityId).IsClustered();
            issuePriority.Property(i => i.Name).HasColumnType(ToVarcharColumn(64));

            issuePriority.HasData(
                new IssuePriority()
                {
                    IssuePriorityId = (int)DomainEnums.IssuePriority.Outage,
                    Name = "Outage",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssuePriority()
                {
                    IssuePriorityId = (int)DomainEnums.IssuePriority.Critical,
                    Name = "Critical",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssuePriority()
                {
                    IssuePriorityId = (int)DomainEnums.IssuePriority.High,
                    Name = "High",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssuePriority()
                {
                    IssuePriorityId = (int)DomainEnums.IssuePriority.Medium,
                    Name = "Medium",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssuePriority()
                {
                    IssuePriorityId = (int)DomainEnums.IssuePriority.Low,
                    Name = "Low",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                }
            );
        }
    }
}

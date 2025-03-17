using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Api.Domain.Entities;
using SharedEnums = ServiceXpert.Shared.Enums;

namespace ServiceXpert.Api.Infrastructure.DbContexts
{
    internal class IssuePriorityDbContext : DbContextBase, IEntityTypeConfiguration<IssuePriority>
    {
        private DateTime dateTime = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc);

        public void Configure(EntityTypeBuilder<IssuePriority> issuePriority)
        {
            issuePriority.ToTable("IssuePriorities");

            issuePriority.HasKey(i => i.IssuePriorityId).IsClustered();
            issuePriority.Property(i => i.Name).HasColumnType(ToVarcharColumn(64));

            issuePriority.HasData(
                new IssuePriority()
                {
                    IssuePriorityId = (int)SharedEnums.IssuePriority.Outage,
                    Name = "Outage",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssuePriority()
                {
                    IssuePriorityId = (int)SharedEnums.IssuePriority.Critical,
                    Name = "Critical",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssuePriority()
                {
                    IssuePriorityId = (int)SharedEnums.IssuePriority.High,
                    Name = "High",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssuePriority()
                {
                    IssuePriorityId = (int)SharedEnums.IssuePriority.Medium,
                    Name = "Medium",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssuePriority()
                {
                    IssuePriorityId = (int)SharedEnums.IssuePriority.Low,
                    Name = "Low",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                }
            );
        }
    }
}

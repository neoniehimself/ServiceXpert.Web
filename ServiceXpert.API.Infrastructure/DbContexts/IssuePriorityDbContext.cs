using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.API.Domain.Entities;
using DomainLayerEnum = ServiceXpert.API.Domain.Shared.Enums;

namespace ServiceXpert.API.Infrastructure.DbContexts
{
    internal class IssuePriorityDbContext : DbContextBase, IEntityTypeConfiguration<IssuePriority>
    {
        private DateTime dateTime = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc);

        public void Configure(EntityTypeBuilder<IssuePriority> issuePriority)
        {
            issuePriority.HasKey(i => i.IssuePriorityID).IsClustered();

            issuePriority.Property(i => i.Name).HasColumnType(ToVarcharColumn(64));
            issuePriority.Property(i => i.Description).HasColumnType(ToVarcharColumn(1024));

            issuePriority.HasData(
                new IssuePriority()
                {
                    IssuePriorityID = DomainLayerEnum.Issue.IssuePriority.Outage,
                    Name = "Outage",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssuePriority()
                {
                    IssuePriorityID = DomainLayerEnum.Issue.IssuePriority.Critical,
                    Name = "Critical",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssuePriority()
                {
                    IssuePriorityID = DomainLayerEnum.Issue.IssuePriority.High,
                    Name = "High",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssuePriority()
                {
                    IssuePriorityID = DomainLayerEnum.Issue.IssuePriority.Medium,
                    Name = "Medium",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssuePriority()
                {
                    IssuePriorityID = DomainLayerEnum.Issue.IssuePriority.Low,
                    Name = "Low",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                }
            );
        }
    }
}

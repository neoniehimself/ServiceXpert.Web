using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Api.Domain.Entities;
using SharedEnums = ServiceXpert.Shared.Enums;

namespace ServiceXpert.Api.Infrastructure.DbContexts
{
    internal class IssueStatusDbContext : DbContextBase, IEntityTypeConfiguration<IssueStatus>
    {
        private DateTime dateTime = new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc);

        public void Configure(EntityTypeBuilder<IssueStatus> issueStatus)
        {
            issueStatus.ToTable("IssueStatuses");

            issueStatus.HasKey(i => i.IssueStatusId).IsClustered();
            issueStatus.Property(i => i.Name).HasColumnType(ToVarcharColumn(64));

            issueStatus.HasData(
                new IssueStatus()
                {
                    IssueStatusId = (int)SharedEnums.IssueStatus.New,
                    Name = "New",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssueStatus()
                {
                    IssueStatusId = (int)SharedEnums.IssueStatus.ForAnalysis,
                    Name = "For Analysis",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssueStatus()
                {
                    IssueStatusId = (int)SharedEnums.IssueStatus.InProgress,
                    Name = "In Progress",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssueStatus()
                {
                    IssueStatusId = (int)SharedEnums.IssueStatus.Resolved,
                    Name = "Resolved",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssueStatus()
                {
                    IssueStatusId = (int)SharedEnums.IssueStatus.Closed,
                    Name = "Closed",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                }
            );
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities;
using SxpEnums = ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Infrastructure.Contexts
{
    internal class IssueStatusDbContext : DbContextBase, IEntityTypeConfiguration<IssueStatus>
    {
        private readonly DateTime dateTime = new(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc);

        public void Configure(EntityTypeBuilder<IssueStatus> issueStatus)
        {
            issueStatus.HasKey(i => i.IssueStatusId).IsClustered();
            issueStatus.Property(i => i.Name).HasColumnType(ToVarcharColumn(64));

            issueStatus.HasData(
                new IssueStatus()
                {
                    IssueStatusId = (int)SxpEnums.IssueStatus.New,
                    Name = "New",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssueStatus()
                {
                    IssueStatusId = (int)SxpEnums.IssueStatus.ForAnalysis,
                    Name = "For Analysis",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssueStatus()
                {
                    IssueStatusId = (int)SxpEnums.IssueStatus.InProgress,
                    Name = "In Progress",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssueStatus()
                {
                    IssueStatusId = (int)SxpEnums.IssueStatus.Resolved,
                    Name = "Resolved",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssueStatus()
                {
                    IssueStatusId = (int)SxpEnums.IssueStatus.Closed,
                    Name = "Closed",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                }
            );
        }
    }
}

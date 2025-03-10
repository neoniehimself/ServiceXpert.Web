using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Api.Domain.Entities;
using DomainLayerEnum = ServiceXpert.Api.Domain.Shared.Enums;

namespace ServiceXpert.Api.Infrastructure.DbContexts
{
    internal class IssueStatusDbContext : DbContextBase, IEntityTypeConfiguration<IssueStatus>
    {
        private DateTime dateTime = new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc);

        public void Configure(EntityTypeBuilder<IssueStatus> issueStatus)
        {
            issueStatus.HasKey(i => i.IssueStatusID).IsClustered();
            issueStatus.Property(i => i.Name).HasColumnType(ToVarcharColumn(64));
            issueStatus.Property(i => i.Description).HasColumnType(ToVarcharColumn(1024));

            issueStatus.HasData(
                new IssueStatus()
                {
                    IssueStatusID = (int)DomainLayerEnum.Issue.IssueStatus.New,
                    Name = "New",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssueStatus()
                {
                    IssueStatusID = (int)DomainLayerEnum.Issue.IssueStatus.ForAnalysis,
                    Name = "For Analysis",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssueStatus()
                {
                    IssueStatusID = (int)DomainLayerEnum.Issue.IssueStatus.InProgress,
                    Name = "In Progress",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssueStatus()
                {
                    IssueStatusID = (int)DomainLayerEnum.Issue.IssueStatus.Resolved,
                    Name = "Resolved",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssueStatus()
                {
                    IssueStatusID = (int)DomainLayerEnum.Issue.IssueStatus.Closed,
                    Name = "Closed",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                }
            );
        }
    }
}

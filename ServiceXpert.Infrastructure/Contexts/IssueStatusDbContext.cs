using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities;
using DomainEnums = ServiceXpert.Domain.Shared.Enums;

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
                    IssueStatusId = (int)DomainEnums.IssueStatus.New,
                    Name = "New",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssueStatus()
                {
                    IssueStatusId = (int)DomainEnums.IssueStatus.ForAnalysis,
                    Name = "For Analysis",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssueStatus()
                {
                    IssueStatusId = (int)DomainEnums.IssueStatus.InProgress,
                    Name = "In Progress",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssueStatus()
                {
                    IssueStatusId = (int)DomainEnums.IssueStatus.Resolved,
                    Name = "Resolved",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                },
                new IssueStatus()
                {
                    IssueStatusId = (int)DomainEnums.IssueStatus.Closed,
                    Name = "Closed",
                    CreateDate = this.dateTime,
                    ModifyDate = this.dateTime
                }
            );
        }
    }
}

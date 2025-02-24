using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Infrastructure.DbContexts
{
    internal class IssueStatusDbContext : DbContextBase, IEntityTypeConfiguration<IssueStatus>
    {
        public void Configure(EntityTypeBuilder<IssueStatus> issueStatus)
        {
            issueStatus.HasKey(i => i.IssueStatusID).IsClustered();
            issueStatus.Property(i => i.Name).HasColumnType(ToVarcharColumn(64));
            issueStatus.Property(i => i.Description).HasColumnType(ToVarcharColumn(1024));

            issueStatus.HasData(
                new IssueStatus()
                {
                    IssueStatusID = 1,
                    Name = "New"
                },
                new IssueStatus()
                {
                    IssueStatusID = 2,
                    Name = "For Analysis"
                },
                new IssueStatus()
                {
                    IssueStatusID = 3,
                    Name = "In Progress"
                },
                new IssueStatus()
                {
                    IssueStatusID = 4,
                    Name = "Resolved"
                },
                new IssueStatus()
                {
                    IssueStatusID = 5,
                    Name = "Closed"
                }
            );
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Api.Domain.Entities;

namespace ServiceXpert.Api.Infrastructure.DbContexts
{
    internal class IssueDbContext : DbContextBase, IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> issue)
        {
            issue.HasKey(i => i.IssueId).IsClustered();

            issue.Property(i => i.Name)
                .HasColumnType(ToVarcharColumn(256));
            issue.Property(i => i.Description)
                .HasColumnType(ToVarcharColumn(4096));

            issue.HasOne(i => i.IssueStatus)
                .WithOne()
                .HasForeignKey<Issue>(i => i.IssueStatusId);

            issue.HasOne(i => i.IssuePriority)
                .WithOne()
                .HasForeignKey<Issue>(i => i.IssuePriorityId);

            issue.Navigation(i => i.IssueStatus).AutoInclude();
            issue.Navigation(i => i.IssuePriority).AutoInclude();
        }
    }
}

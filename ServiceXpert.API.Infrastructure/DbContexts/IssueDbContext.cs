using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Infrastructure.DbContexts
{
    internal class IssueDbContext : DbContextBase, IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> issue)
        {
            issue.HasKey(i => i.IssueID).IsClustered();

            issue.Property(i => i.Name)
                .HasColumnType(ToVarcharColumn(256));
            issue.Property(i => i.Description)
                .HasColumnType(ToVarcharColumn(4096));

            issue.HasOne(i => i.IssueStatus)
                .WithOne()
                .HasForeignKey<Issue>(i => i.IssueStatusID);
        }
    }
}

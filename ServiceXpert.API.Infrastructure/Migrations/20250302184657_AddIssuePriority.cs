using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServiceXpert.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIssuePriority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IssuePriorityID",
                table: "Issue",
                type: "int",
                nullable: false,
                defaultValue: Domain.Shared.Enums.Issue.IssuePriority.Low);

            migrationBuilder.CreateTable(
                name: "IssuePriority",
                columns: table => new
                {
                    IssuePriorityID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(1024)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuePriority", x => x.IssuePriorityID)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.InsertData(
                table: "IssuePriority",
                columns: new[] { "IssuePriorityID", "CreateDate", "Description", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Outage" },
                    { 2, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Critical" },
                    { 3, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "High" },
                    { 4, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Medium" },
                    { 5, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Low" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_IssuePriority_IssuePriorityID",
                table: "Issue",
                column: "IssuePriorityID",
                principalTable: "IssuePriority",
                principalColumn: "IssuePriorityID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_IssuePriority_IssuePriorityID",
                table: "Issue");

            migrationBuilder.DropTable(
                name: "IssuePriority");

            migrationBuilder.DropColumn(
                name: "IssuePriorityID",
                table: "Issue");
        }
    }
}

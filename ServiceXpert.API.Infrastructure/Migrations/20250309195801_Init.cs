using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServiceXpert.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IssuePriority",
                columns: table => new
                {
                    IssuePriorityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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

            migrationBuilder.CreateTable(
                name: "IssueStatus",
                columns: table => new
                {
                    IssueStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(1024)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueStatus", x => x.IssueStatusID)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Issue",
                columns: table => new
                {
                    IssueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(256)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(4096)", nullable: true),
                    IssueStatusID = table.Column<int>(type: "int", nullable: false),
                    IssuePriorityID = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issue", x => x.IssueID)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Issue_IssuePriority_IssuePriorityID",
                        column: x => x.IssuePriorityID,
                        principalTable: "IssuePriority",
                        principalColumn: "IssuePriorityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issue_IssueStatus_IssueStatusID",
                        column: x => x.IssueStatusID,
                        principalTable: "IssueStatus",
                        principalColumn: "IssueStatusID",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.InsertData(
                table: "IssueStatus",
                columns: new[] { "IssueStatusID", "CreateDate", "Description", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "New" },
                    { 2, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "For Analysis" },
                    { 3, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "In Progress" },
                    { 4, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Resolved" },
                    { 5, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Closed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Issue");

            migrationBuilder.DropTable(
                name: "IssuePriority");

            migrationBuilder.DropTable(
                name: "IssueStatus");
        }
    }
}

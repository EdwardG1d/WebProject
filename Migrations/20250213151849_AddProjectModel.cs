using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProject.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_Takss_TaskId",
                table: "TimeEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeEntry",
                table: "TimeEntry");

            migrationBuilder.RenameTable(
                name: "TimeEntry",
                newName: "TimeEntries");

            migrationBuilder.RenameIndex(
                name: "IX_TimeEntry_TaskId",
                table: "TimeEntries",
                newName: "IX_TimeEntries_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeEntry_Date_TaskId",
                table: "TimeEntries",
                newName: "IX_TimeEntries_Date_TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeEntries",
                table: "TimeEntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntries_Takss_TaskId",
                table: "TimeEntries",
                column: "TaskId",
                principalTable: "Takss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntries_Takss_TaskId",
                table: "TimeEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeEntries",
                table: "TimeEntries");

            migrationBuilder.RenameTable(
                name: "TimeEntries",
                newName: "TimeEntry");

            migrationBuilder.RenameIndex(
                name: "IX_TimeEntries_TaskId",
                table: "TimeEntry",
                newName: "IX_TimeEntry_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeEntries_Date_TaskId",
                table: "TimeEntry",
                newName: "IX_TimeEntry_Date_TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeEntry",
                table: "TimeEntry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_Takss_TaskId",
                table: "TimeEntry",
                column: "TaskId",
                principalTable: "Takss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

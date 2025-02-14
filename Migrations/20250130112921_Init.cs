using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProject.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_Takss_TaskId",
                table: "TimeEntry");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TimeEntry",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_Takss_TaskId",
                table: "TimeEntry",
                column: "TaskId",
                principalTable: "Takss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_Takss_TaskId",
                table: "TimeEntry");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TimeEntry",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_Takss_TaskId",
                table: "TimeEntry",
                column: "TaskId",
                principalTable: "Takss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

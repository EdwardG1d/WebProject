using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntries_Takss_TaskId",
                table: "TimeEntries");

            migrationBuilder.DropTable(
                name: "Takss");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TimeEntries",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntries_Tasks_TaskId",
                table: "TimeEntries",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntries_Tasks_TaskId",
                table: "TimeEntries");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TimeEntries",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "Takss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Takss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Takss_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Takss_ProjectId",
                table: "Takss",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntries_Takss_TaskId",
                table: "TimeEntries",
                column: "TaskId",
                principalTable: "Takss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

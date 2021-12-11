using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projects_students_StudentId",
                table: "projects");

            migrationBuilder.DropIndex(
                name: "IX_projects_StudentId",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "projects");

            migrationBuilder.CreateTable(
                name: "ProjectStudent",
                columns: table => new
                {
                    ApplicationsId = table.Column<int>(type: "int", nullable: false),
                    AppliedProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStudent", x => new { x.ApplicationsId, x.AppliedProjectsId });
                    table.ForeignKey(
                        name: "FK_ProjectStudent_projects_AppliedProjectsId",
                        column: x => x.AppliedProjectsId,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectStudent_students_ApplicationsId",
                        column: x => x.ApplicationsId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStudent_AppliedProjectsId",
                table: "ProjectStudent",
                column: "AppliedProjectsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectStudent");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "projects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_projects_StudentId",
                table: "projects",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_projects_students_StudentId",
                table: "projects",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id");
        }
    }
}

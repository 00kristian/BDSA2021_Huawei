using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class preferences2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projects_Location_LocationId",
                table: "projects");

            migrationBuilder.RenameColumn(
                name: "isThesis",
                table: "projects",
                newName: "IsThesis");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "Location",
                newName: "Str");

            migrationBuilder.AddColumn<int>(
                name: "Degree",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PreferenceId",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "University",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "projects",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
                name: "FK_projects_Location_LocationId",
                table: "projects",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_projects_students_StudentId",
                table: "projects",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projects_Location_LocationId",
                table: "projects");

            migrationBuilder.DropForeignKey(
                name: "FK_projects_students_StudentId",
                table: "projects");

            migrationBuilder.DropIndex(
                name: "IX_projects_StudentId",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "Degree",
                table: "students");

            migrationBuilder.DropColumn(
                name: "PreferenceId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "University",
                table: "students");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "projects");

            migrationBuilder.RenameColumn(
                name: "IsThesis",
                table: "projects",
                newName: "isThesis");

            migrationBuilder.RenameColumn(
                name: "Str",
                table: "Location",
                newName: "Day");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_projects_Location_LocationId",
                table: "projects",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class modelbuilder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Preferences_PreferencesId",
                table: "Location");

            migrationBuilder.DropTable(
                name: "Workday");

            migrationBuilder.DropIndex(
                name: "IX_Location_PreferencesId",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "PreferenceId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "PreferencesId",
                table: "Location");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "Preferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Locations",
                table: "Preferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Preferences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Workdays",
                table: "Preferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Preferences_StudentId",
                table: "Preferences",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Preferences_students_StudentId",
                table: "Preferences",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Preferences_students_StudentId",
                table: "Preferences");

            migrationBuilder.DropIndex(
                name: "IX_Preferences_StudentId",
                table: "Preferences");

            migrationBuilder.DropColumn(
                name: "Locations",
                table: "Preferences");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Preferences");

            migrationBuilder.DropColumn(
                name: "Workdays",
                table: "Preferences");

            migrationBuilder.AddColumn<int>(
                name: "PreferenceId",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "Preferences",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PreferencesId",
                table: "Location",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Workday",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferencesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workday", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workday_Preferences_PreferencesId",
                        column: x => x.PreferencesId,
                        principalTable: "Preferences",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_PreferencesId",
                table: "Location",
                column: "PreferencesId");

            migrationBuilder.CreateIndex(
                name: "IX_Workday_PreferencesId",
                table: "Workday",
                column: "PreferencesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Preferences_PreferencesId",
                table: "Location",
                column: "PreferencesId",
                principalTable: "Preferences",
                principalColumn: "Id");
        }
    }
}

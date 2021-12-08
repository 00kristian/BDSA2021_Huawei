using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class preferencesnice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeywordPreferences_Preferences_PreferencesId",
                table: "KeywordPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Preferences_students_StudentId",
                table: "Preferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Preferences",
                table: "Preferences");

            migrationBuilder.RenameTable(
                name: "Preferences",
                newName: "preferences");

            migrationBuilder.RenameIndex(
                name: "IX_Preferences_StudentId",
                table: "preferences",
                newName: "IX_preferences_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_preferences",
                table: "preferences",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordPreferences_preferences_PreferencesId",
                table: "KeywordPreferences",
                column: "PreferencesId",
                principalTable: "preferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_preferences_students_StudentId",
                table: "preferences",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeywordPreferences_preferences_PreferencesId",
                table: "KeywordPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_preferences_students_StudentId",
                table: "preferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_preferences",
                table: "preferences");

            migrationBuilder.RenameTable(
                name: "preferences",
                newName: "Preferences");

            migrationBuilder.RenameIndex(
                name: "IX_preferences_StudentId",
                table: "Preferences",
                newName: "IX_Preferences_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Preferences",
                table: "Preferences",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordPreferences_Preferences_PreferencesId",
                table: "KeywordPreferences",
                column: "PreferencesId",
                principalTable: "Preferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Preferences_students_StudentId",
                table: "Preferences",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

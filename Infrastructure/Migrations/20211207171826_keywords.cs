using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class keywords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeywordPreferences_Keyword_KeywordsId",
                table: "KeywordPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_KeywordPreferences_Preferences_StudentsId",
                table: "KeywordPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_KeywordProject_Keyword_KeywordsId",
                table: "KeywordProject");

            migrationBuilder.DropForeignKey(
                name: "FK_projects_Location_LocationId",
                table: "projects");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_projects_LocationId",
                table: "projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Keyword",
                table: "Keyword");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "projects");

            migrationBuilder.RenameTable(
                name: "Keyword",
                newName: "keywords");

            migrationBuilder.RenameColumn(
                name: "StudentsId",
                table: "KeywordPreferences",
                newName: "PreferencesId");

            migrationBuilder.RenameIndex(
                name: "IX_KeywordPreferences_StudentsId",
                table: "KeywordPreferences",
                newName: "IX_KeywordPreferences_PreferencesId");

            migrationBuilder.AddColumn<int>(
                name: "Location",
                table: "projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Str",
                table: "keywords",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_keywords",
                table: "keywords",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_keywords_Str",
                table: "keywords",
                column: "Str",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordPreferences_keywords_KeywordsId",
                table: "KeywordPreferences",
                column: "KeywordsId",
                principalTable: "keywords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordPreferences_Preferences_PreferencesId",
                table: "KeywordPreferences",
                column: "PreferencesId",
                principalTable: "Preferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordProject_keywords_KeywordsId",
                table: "KeywordProject",
                column: "KeywordsId",
                principalTable: "keywords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeywordPreferences_keywords_KeywordsId",
                table: "KeywordPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_KeywordPreferences_Preferences_PreferencesId",
                table: "KeywordPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_KeywordProject_keywords_KeywordsId",
                table: "KeywordProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_keywords",
                table: "keywords");

            migrationBuilder.DropIndex(
                name: "IX_keywords_Str",
                table: "keywords");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "projects");

            migrationBuilder.RenameTable(
                name: "keywords",
                newName: "Keyword");

            migrationBuilder.RenameColumn(
                name: "PreferencesId",
                table: "KeywordPreferences",
                newName: "StudentsId");

            migrationBuilder.RenameIndex(
                name: "IX_KeywordPreferences_PreferencesId",
                table: "KeywordPreferences",
                newName: "IX_KeywordPreferences_StudentsId");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "projects",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Str",
                table: "Keyword",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Keyword",
                table: "Keyword",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Str = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_projects_LocationId",
                table: "projects",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordPreferences_Keyword_KeywordsId",
                table: "KeywordPreferences",
                column: "KeywordsId",
                principalTable: "Keyword",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordPreferences_Preferences_StudentsId",
                table: "KeywordPreferences",
                column: "StudentsId",
                principalTable: "Preferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordProject_Keyword_KeywordsId",
                table: "KeywordProject",
                column: "KeywordsId",
                principalTable: "Keyword",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_projects_Location_LocationId",
                table: "projects",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id");
        }
    }
}

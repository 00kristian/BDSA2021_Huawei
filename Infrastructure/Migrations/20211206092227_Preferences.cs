using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Preferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "projects",
                newName: "LocationId");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Preferences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferencesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Preferences_PreferencesId",
                        column: x => x.PreferencesId,
                        principalTable: "Preferences",
                        principalColumn: "Id");
                });

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
                name: "IX_projects_LocationId",
                table: "projects",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_PreferencesId",
                table: "Location",
                column: "PreferencesId");

            migrationBuilder.CreateIndex(
                name: "IX_Workday_PreferencesId",
                table: "Workday",
                column: "PreferencesId");

            migrationBuilder.AddForeignKey(
                name: "FK_projects_Location_LocationId",
                table: "projects",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projects_Location_LocationId",
                table: "projects");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Workday");

            migrationBuilder.DropIndex(
                name: "IX_projects_LocationId",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Preferences");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "projects",
                newName: "Location");

            migrationBuilder.AlterColumn<int>(
                name: "Language",
                table: "projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}

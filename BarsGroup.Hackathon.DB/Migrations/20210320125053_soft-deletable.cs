using Microsoft.EntityFrameworkCore.Migrations;

namespace BarsGroup.Hackathon.Db.Migrations
{
    public partial class softdeletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "hackathon",
                table: "files",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "hackathon",
                table: "files");
        }
    }
}

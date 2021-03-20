using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BarsGroup.Hackathon.Db.Migrations
{
    public partial class seed_admin_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "hackathon",
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[] { new Guid("a18be9c0-aa65-4af8-bd17-00bd9344e575"), "acd20ec8-8043-4c48-9219-aeb2f7b56680", "admin", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "hackathon",
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: new Guid("a18be9c0-aa65-4af8-bd17-00bd9344e575"));
        }
    }
}

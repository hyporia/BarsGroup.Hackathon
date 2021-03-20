using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BarsGroup.Hackathon.Db.Migrations
{
	public partial class user_files : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "address",
				schema: "hackathon",
				table: "files",
				type: "text",
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<string>(
				name: "content_type",
				schema: "hackathon",
				table: "files",
				type: "text",
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<string>(
				name: "name",
				schema: "hackathon",
				table: "files",
				type: "text",
				nullable: true);

			migrationBuilder.AddColumn<int>(
				name: "size",
				schema: "hackathon",
				table: "files",
				type: "integer",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddColumn<Guid>(
				name: "user_id",
				schema: "hackathon",
				table: "files",
				type: "uuid",
				nullable: false,
				defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

			migrationBuilder.CreateIndex(
				name: "ix_files_user_id",
				schema: "hackathon",
				table: "files",
				column: "user_id");

			migrationBuilder.AddForeignKey(
				name: "fk_files_users_user_id",
				schema: "hackathon",
				table: "files",
				column: "user_id",
				principalSchema: "hackathon",
				principalTable: "AspNetUsers",
				principalColumn: "id",
				onDelete: ReferentialAction.Restrict);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "fk_files_users_user_id",
				schema: "hackathon",
				table: "files");

			migrationBuilder.DropIndex(
				name: "ix_files_user_id",
				schema: "hackathon",
				table: "files");

			migrationBuilder.DropColumn(
				name: "address",
				schema: "hackathon",
				table: "files");

			migrationBuilder.DropColumn(
				name: "content_type",
				schema: "hackathon",
				table: "files");

			migrationBuilder.DropColumn(
				name: "name",
				schema: "hackathon",
				table: "files");

			migrationBuilder.DropColumn(
				name: "size",
				schema: "hackathon",
				table: "files");

			migrationBuilder.DropColumn(
				name: "user_id",
				schema: "hackathon",
				table: "files");
		}
	}
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryAPI.Migrations
{
    public partial class AddPrimaryKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusHistory",
                table: "StatusHistory",
                columns: new[] { "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor",
                columns: new[] { "BookId", "AuthorId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusHistory",
                table: "StatusHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor");
        }
    }
}

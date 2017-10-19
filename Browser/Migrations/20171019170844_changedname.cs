using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Browser.Migrations
{
    public partial class changedname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Url_UrlID",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Url_UrlID",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Favorites");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "History");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UrlID",
                table: "Favorites",
                newName: "IX_Favorites_UrlID");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_UrlID",
                table: "History",
                newName: "IX_History_UrlID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_History",
                table: "History",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Url_UrlID",
                table: "Favorites",
                column: "UrlID",
                principalTable: "Url",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_History_Url_UrlID",
                table: "History",
                column: "UrlID",
                principalTable: "Url",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Url_UrlID",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_History_Url_UrlID",
                table: "History");

            migrationBuilder.DropPrimaryKey(
                name: "PK_History",
                table: "History");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.RenameTable(
                name: "History",
                newName: "Blogs");

            migrationBuilder.RenameTable(
                name: "Favorites",
                newName: "Posts");

            migrationBuilder.RenameIndex(
                name: "IX_History_UrlID",
                table: "Blogs",
                newName: "IX_Blogs_UrlID");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_UrlID",
                table: "Posts",
                newName: "IX_Posts_UrlID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Url_UrlID",
                table: "Blogs",
                column: "UrlID",
                principalTable: "Url",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Url_UrlID",
                table: "Posts",
                column: "UrlID",
                principalTable: "Url",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

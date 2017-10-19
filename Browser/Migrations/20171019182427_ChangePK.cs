using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Browser.Migrations
{
    public partial class ChangePK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Url_UrlID",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_History_Url_UrlID",
                table: "History");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Url",
                table: "Url");

            migrationBuilder.DropPrimaryKey(
                name: "PK_History",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_History_UrlID",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_UrlID",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Url");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "History");

            migrationBuilder.DropColumn(
                name: "UrlID",
                table: "History");

            migrationBuilder.DropColumn(
                name: "UrlID",
                table: "Favorites");

            migrationBuilder.AlterColumn<string>(
                name: "Host",
                table: "Url",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlHost",
                table: "History",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlHost",
                table: "Favorites",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Url",
                table: "Url",
                column: "Host");

            migrationBuilder.AddPrimaryKey(
                name: "PK_History",
                table: "History",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_History_UrlHost",
                table: "History",
                column: "UrlHost");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UrlHost",
                table: "Favorites",
                column: "UrlHost");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Url_UrlHost",
                table: "Favorites",
                column: "UrlHost",
                principalTable: "Url",
                principalColumn: "Host",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_History_Url_UrlHost",
                table: "History",
                column: "UrlHost",
                principalTable: "Url",
                principalColumn: "Host",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Url_UrlHost",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_History_Url_UrlHost",
                table: "History");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Url",
                table: "Url");

            migrationBuilder.DropPrimaryKey(
                name: "PK_History",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_History_UrlHost",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_UrlHost",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "UrlHost",
                table: "History");

            migrationBuilder.DropColumn(
                name: "UrlHost",
                table: "Favorites");

            migrationBuilder.AlterColumn<string>(
                name: "Host",
                table: "Url",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "Url",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "History",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UrlID",
                table: "History",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UrlID",
                table: "Favorites",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Url",
                table: "Url",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_History",
                table: "History",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_History_UrlID",
                table: "History",
                column: "UrlID");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UrlID",
                table: "Favorites",
                column: "UrlID");

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
    }
}

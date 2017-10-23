using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Browser.Migrations
{
    public partial class ChangePK2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_History_UrlHost",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_UrlHost",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "UrlHost",
                table: "History");

            migrationBuilder.AlterColumn<string>(
                name: "Unidentified",
                table: "Url",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Scheme",
                table: "Url",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Url",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlAddon",
                table: "Favorites",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlScheme",
                table: "Favorites",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlUnidentified",
                table: "Favorites",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Url_Host",
                table: "Url",
                column: "Host");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Url",
                table: "Url",
                columns: new[] { "Scheme", "Host", "Path", "Unidentified" });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UrlScheme_UrlHost_UrlAddon_UrlUnidentified",
                table: "Favorites",
                columns: new[] { "UrlScheme", "UrlHost", "UrlAddon", "UrlUnidentified" });

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Url_UrlScheme_UrlHost_UrlAddon_UrlUnidentified",
                table: "Favorites",
                columns: new[] { "UrlScheme", "UrlHost", "UrlAddon", "UrlUnidentified" },
                principalTable: "Url",
                principalColumns: new[] { "Scheme", "Host", "Path", "Unidentified" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Url_UrlScheme_UrlHost_UrlAddon_UrlUnidentified",
                table: "Favorites");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Url_Host",
                table: "Url");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Url",
                table: "Url");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_UrlScheme_UrlHost_UrlAddon_UrlUnidentified",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "UrlAddon",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "UrlScheme",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "UrlUnidentified",
                table: "Favorites");

            migrationBuilder.AlterColumn<string>(
                name: "Unidentified",
                table: "Url",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Url",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Scheme",
                table: "Url",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "UrlHost",
                table: "History",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Url",
                table: "Url",
                column: "Host");

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
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicAppRedux.Migrations
{
    public partial class FavGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenreID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GenreID",
                table: "AspNetUsers",
                column: "GenreID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Genres_GenreID",
                table: "AspNetUsers",
                column: "GenreID",
                principalTable: "Genres",
                principalColumn: "GenreID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Genres_GenreID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GenreID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GenreID",
                table: "AspNetUsers");
        }
    }
}

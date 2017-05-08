using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicAppRedux.Migrations
{
    public partial class AuthMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GenreID",
                table: "AspNetUsers",
                newName: "UserFavoriteGenreID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserFavoriteGenreID",
                table: "AspNetUsers",
                newName: "GenreID");
        }
    }
}

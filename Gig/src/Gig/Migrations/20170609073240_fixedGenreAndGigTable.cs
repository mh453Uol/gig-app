using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gig.Migrations
{
    public partial class fixedGenreAndGigTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Venue",
                table: "Gigs",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                maxLength: 255,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Venue",
                table: "Gigs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                nullable: true);
        }
    }
}

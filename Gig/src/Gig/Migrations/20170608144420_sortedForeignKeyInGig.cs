using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gig.Migrations
{
    public partial class sortedForeignKeyInGig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gigs_AspNetUsers_ArtistId1",
                table: "Gigs");

            migrationBuilder.DropForeignKey(
                name: "FK_Gigs_Genres_GenreId1",
                table: "Gigs");

            migrationBuilder.DropIndex(
                name: "IX_Gigs_ArtistId1",
                table: "Gigs");

            migrationBuilder.DropIndex(
                name: "IX_Gigs_GenreId1",
                table: "Gigs");

            migrationBuilder.DropColumn(
                name: "ArtistId1",
                table: "Gigs");

            migrationBuilder.DropColumn(
                name: "GenreId1",
                table: "Gigs");

            migrationBuilder.AlterColumn<byte>(
                name: "GenreId",
                table: "Gigs",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "ArtistId",
                table: "Gigs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gigs_ArtistId",
                table: "Gigs",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Gigs_GenreId",
                table: "Gigs",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gigs_AspNetUsers_ArtistId",
                table: "Gigs",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gigs_Genres_GenreId",
                table: "Gigs",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gigs_AspNetUsers_ArtistId",
                table: "Gigs");

            migrationBuilder.DropForeignKey(
                name: "FK_Gigs_Genres_GenreId",
                table: "Gigs");

            migrationBuilder.DropIndex(
                name: "IX_Gigs_ArtistId",
                table: "Gigs");

            migrationBuilder.DropIndex(
                name: "IX_Gigs_GenreId",
                table: "Gigs");

            migrationBuilder.AddColumn<string>(
                name: "ArtistId1",
                table: "Gigs",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "GenreId1",
                table: "Gigs",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "Gigs",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "ArtistId",
                table: "Gigs",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Gigs_ArtistId1",
                table: "Gigs",
                column: "ArtistId1");

            migrationBuilder.CreateIndex(
                name: "IX_Gigs_GenreId1",
                table: "Gigs",
                column: "GenreId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Gigs_AspNetUsers_ArtistId1",
                table: "Gigs",
                column: "ArtistId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gigs_Genres_GenreId1",
                table: "Gigs",
                column: "GenreId1",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

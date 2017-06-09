using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gig.Migrations
{
    public partial class populateGenresTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Genres (Name) VALUES ('Jazz')");
            migrationBuilder.Sql("INSERT INTO Genres (Name) VALUES ('Blues')");
            migrationBuilder.Sql("INSERT INTO Genres (Name) VALUES ('Rock')");
            migrationBuilder.Sql("INSERT INTO Genres (Name) VALUES ('Country')");
            migrationBuilder.Sql("INSERT INTO Genres (Name) VALUES ('Rap')");
            migrationBuilder.Sql("INSERT INTO Genres (Name) VALUES ('Trap')");
            migrationBuilder.Sql("INSERT INTO Genres (Name) VALUES ('Hiphop')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Genres Where Name('Jazz',Jazz'Blues','Rock','Country','Rap','Trap','Hiphop')");
        }                                                              
    }                                                                  
}                                                                     
                                                                     
                                                                       
                                                                      
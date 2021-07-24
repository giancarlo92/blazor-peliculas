using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorPeliculas.Server.Migrations
{
    public partial class VotosPeliculas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VotoPelicula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voto = table.Column<int>(type: "int", nullable: false),
                    FechaVoto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeliculaId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotoPelicula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VotoPelicula_Pelicula_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Pelicula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VotoPelicula_PeliculaId",
                table: "VotoPelicula",
                column: "PeliculaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VotoPelicula");
        }
    }
}

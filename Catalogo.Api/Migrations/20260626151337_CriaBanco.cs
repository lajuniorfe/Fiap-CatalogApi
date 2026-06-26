using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogo.Api.Migrations
{
    /// <inheritdoc />
    public partial class CriaBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "biblioteca",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_biblioteca", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "jogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jogo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bibliotecaJogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataAquisicao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BibliotecaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    JogoId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bibliotecaJogo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bibliotecaJogo_biblioteca_BibliotecaId",
                        column: x => x.BibliotecaId,
                        principalTable: "biblioteca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bibliotecaJogo_jogo_JogoId",
                        column: x => x.JogoId,
                        principalTable: "jogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bibliotecaJogo_BibliotecaId",
                table: "bibliotecaJogo",
                column: "BibliotecaId");

            migrationBuilder.CreateIndex(
                name: "IX_bibliotecaJogo_JogoId",
                table: "bibliotecaJogo",
                column: "JogoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bibliotecaJogo");

            migrationBuilder.DropTable(
                name: "biblioteca");

            migrationBuilder.DropTable(
                name: "jogo");
        }
    }
}

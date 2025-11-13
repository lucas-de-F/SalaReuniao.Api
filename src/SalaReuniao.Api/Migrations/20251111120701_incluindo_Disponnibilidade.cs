using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaReuniao.Api.Migrations
{
    /// <inheritdoc />
    public partial class incluindo_Disponnibilidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DisponibilidadeEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SalaDeReuniaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiaSemana = table.Column<int>(type: "integer", nullable: false),
                    Inicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Fim = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisponibilidadeEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisponibilidadeEntity_Salas_SalaDeReuniaoId",
                        column: x => x.SalaDeReuniaoId,
                        principalTable: "Salas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisponibilidadeEntity_SalaDeReuniaoId",
                table: "DisponibilidadeEntity",
                column: "SalaDeReuniaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisponibilidadeEntity");
        }
    }
}

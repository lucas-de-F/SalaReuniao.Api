using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaReuniao.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Responsaveis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsaveis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdResponsavel = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    IdEndereco = table.Column<Guid>(type: "uuid", nullable: false),
                    ValorHora = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salas_Responsaveis_IdResponsavel",
                        column: x => x.IdResponsavel,
                        principalTable: "Responsaveis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Unidade = table.Column<string>(type: "text", nullable: false),
                    Categoria = table.Column<string>(type: "text", nullable: false),
                    IdResponsavel = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servicos_Responsaveis_IdResponsavel",
                        column: x => x.IdResponsavel,
                        principalTable: "Responsaveis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reunioes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSalaReuniao = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCliente = table.Column<Guid>(type: "uuid", nullable: false),
                    Inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Fim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reunioes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reunioes_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reunioes_Salas_IdSalaReuniao",
                        column: x => x.IdSalaReuniao,
                        principalTable: "Salas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicosOferecidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdServico = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSala = table.Column<Guid>(type: "uuid", nullable: false),
                    Preco = table.Column<decimal>(type: "numeric", nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicosOferecidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicosOferecidos_Salas_IdSala",
                        column: x => x.IdSala,
                        principalTable: "Salas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicosOferecidos_Servicos_IdServico",
                        column: x => x.IdServico,
                        principalTable: "Servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicosAgendados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSalaServicoOferecido = table.Column<Guid>(type: "uuid", nullable: false),
                    IdReuniaoAgendada = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicosAgendados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicosAgendados_Reunioes_IdReuniaoAgendada",
                        column: x => x.IdReuniaoAgendada,
                        principalTable: "Reunioes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicosAgendados_ServicosOferecidos_IdSalaServicoOferecido",
                        column: x => x.IdSalaServicoOferecido,
                        principalTable: "ServicosOferecidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reunioes_IdCliente",
                table: "Reunioes",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Reunioes_IdSalaReuniao",
                table: "Reunioes",
                column: "IdSalaReuniao");

            migrationBuilder.CreateIndex(
                name: "IX_Salas_IdResponsavel",
                table: "Salas",
                column: "IdResponsavel");

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_IdResponsavel",
                table: "Servicos",
                column: "IdResponsavel");

            migrationBuilder.CreateIndex(
                name: "IX_ServicosAgendados_IdReuniaoAgendada",
                table: "ServicosAgendados",
                column: "IdReuniaoAgendada");

            migrationBuilder.CreateIndex(
                name: "IX_ServicosAgendados_IdSalaServicoOferecido",
                table: "ServicosAgendados",
                column: "IdSalaServicoOferecido");

            migrationBuilder.CreateIndex(
                name: "IX_ServicosOferecidos_IdSala",
                table: "ServicosOferecidos",
                column: "IdSala");

            migrationBuilder.CreateIndex(
                name: "IX_ServicosOferecidos_IdServico",
                table: "ServicosOferecidos",
                column: "IdServico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServicosAgendados");

            migrationBuilder.DropTable(
                name: "Reunioes");

            migrationBuilder.DropTable(
                name: "ServicosOferecidos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Salas");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropTable(
                name: "Responsaveis");
        }
    }
}

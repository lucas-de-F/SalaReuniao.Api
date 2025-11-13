using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaReuniao.Api.Migrations
{
    /// <inheritdoc />
    public partial class corrigindo_mapeamento_tabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reunioes_Clientes_IdCliente",
                table: "Reunioes");

            migrationBuilder.DropForeignKey(
                name: "FK_Salas_Responsaveis_IdResponsavel",
                table: "Salas");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Responsaveis_IdResponsavel",
                table: "Servicos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Responsaveis",
                table: "Responsaveis");

            migrationBuilder.RenameTable(
                name: "Responsaveis",
                newName: "Usuarios");

            migrationBuilder.AddColumn<string>(
                name: "TipoUsuario",
                table: "Usuarios",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reunioes_Usuarios_IdCliente",
                table: "Reunioes",
                column: "IdCliente",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salas_Usuarios_IdResponsavel",
                table: "Salas",
                column: "IdResponsavel",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Usuarios_IdResponsavel",
                table: "Servicos",
                column: "IdResponsavel",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reunioes_Usuarios_IdCliente",
                table: "Reunioes");

            migrationBuilder.DropForeignKey(
                name: "FK_Salas_Usuarios_IdResponsavel",
                table: "Salas");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Usuarios_IdResponsavel",
                table: "Servicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "TipoUsuario",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Responsaveis");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Responsaveis",
                table: "Responsaveis",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Reunioes_Clientes_IdCliente",
                table: "Reunioes",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salas_Responsaveis_IdResponsavel",
                table: "Salas",
                column: "IdResponsavel",
                principalTable: "Responsaveis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Responsaveis_IdResponsavel",
                table: "Servicos",
                column: "IdResponsavel",
                principalTable: "Responsaveis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

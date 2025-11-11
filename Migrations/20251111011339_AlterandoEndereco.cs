using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaReuniao.Api.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdEndereco",
                table: "Salas");

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Salas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "Salas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Capacidade",
                table: "Salas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Salas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Salas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Salas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "Salas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rua",
                table: "Salas",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "CEP",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "Capacidade",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "Rua",
                table: "Salas");

            migrationBuilder.AddColumn<Guid>(
                name: "IdEndereco",
                table: "Salas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaReuniao.Api.Migrations
{
    /// <inheritdoc />
    public partial class ajustando_endereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Endereco_Municipio",
                table: "Salas",
                newName: "Localidade");

            migrationBuilder.RenameColumn(
                name: "Cidade",
                table: "Salas",
                newName: "Endereco_Complemento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Localidade",
                table: "Salas",
                newName: "Endereco_Municipio");

            migrationBuilder.RenameColumn(
                name: "Endereco_Complemento",
                table: "Salas",
                newName: "Cidade");
        }
    }
}

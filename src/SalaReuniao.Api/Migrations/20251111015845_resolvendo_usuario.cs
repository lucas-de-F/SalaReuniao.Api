using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaReuniao.Api.Migrations
{
    /// <inheritdoc />
    public partial class resolvendo_usuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoUsuario",
                table: "Usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoUsuario",
                table: "Usuarios",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }
    }
}
